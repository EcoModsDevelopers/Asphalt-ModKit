﻿using Asphalt.Service.Permissions;
using Asphalt.Storeable;
using Asphalt.Util;
using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Asphalt.Service
{
    public class ServiceHelper
    {
        private static object mLocker = new object();
        private static bool mInjected = false;

        internal static void CallMethod(string pName)
        {
            Eco.Core.PluginManager.Controller.ForEach(s => CallMethod(s, pName));
        }

        private static void CallMethod(IServerPlugin pServerPlugin, string pName)
        {
            if (!IsAsphaltPlugin(pServerPlugin.GetType()))
                return;

            MethodInfo mi = pServerPlugin.GetType().GetMethod(pName);
            if (mi == null)
                return;

            mi.Invoke(pServerPlugin, new object[] { });
        }

        public static void InjectValues()
        {
            lock (mLocker)
            {
                if (mInjected)
                    return;
                mInjected = true;

                try //can be removed in the future, after 7.5 is out, because ServicePatches has already try catch
                {
                    typeof(IModKitPlugin).CreatableTypes().ForEach(pluginType => InjectValues(pluginType));
                }
                catch (Exception e)
                {
                    Log.WriteError(e.ToString());
                    throw;
                }
            }
        }

        private static void InjectValues(Type pServerPlugin)
        {
            if (!IsAsphaltPlugin(pServerPlugin))
                return;

            foreach (PropertyFieldInfo pfi in ReflectionUtil.GetPropertyFieldInfos(pServerPlugin, typeof(IPermissionService)))
                InjectPermissions(pServerPlugin, pfi);

            foreach (PropertyFieldInfo pfi in ReflectionUtil.GetPropertyFieldInfos(pServerPlugin, typeof(IStorage)))
                Inject(pServerPlugin, StorageFactory.GetStorageFactory(pServerPlugin, typeof(IStorage)), pfi);

            foreach (PropertyFieldInfo pfi in ReflectionUtil.GetPropertyFieldInfos(pServerPlugin, typeof(IStorageCollection)))
                Inject(pServerPlugin, StorageFactory.GetStorageFactory(pServerPlugin, typeof(IStorageCollection)), pfi);

            foreach (PropertyFieldInfo pfi in ReflectionUtil.GetPropertyFieldInfos(pServerPlugin, typeof(IUserStorageCollection)))
                Inject(pServerPlugin, StorageFactory.GetStorageFactory(pServerPlugin, typeof(IUserStorageCollection)), pfi);
        }

        private static void InjectPermissions(Type pServerPlugin, PropertyFieldInfo pfi)
        {
            const string DefaultPermissionsMethodName = "GetDefaultPermissions";

            if (!pfi.HasInjectAttribute())
                return;

            MethodInfo mi = pServerPlugin.GetMethod(DefaultPermissionsMethodName);

            if (mi == null)
                throw new Exception($"{pServerPlugin} does not implement a public method {DefaultPermissionsMethodName}()");

            if (!mi.IsStatic)
                throw new Exception($"{pServerPlugin}.{DefaultPermissionsMethodName}' needs to be static");

            object permissionList = mi.Invoke(pServerPlugin, new object[] { });

            DefaultPermission[] permissions = permissionList as DefaultPermission[];

            if (permissions == null)
                throw new Exception($"{pServerPlugin}.{DefaultPermissionsMethodName}() does not have the corrent return type {nameof(DefaultPermission)}[] ");

            var storage = StorageFactory.GetStorageFactory(pServerPlugin, typeof(IPermissionService)).Invoke(pfi, permissions.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));
            pfi.SetValue(null, storage);
        }

        private static void Inject(Type pServerPlugin, Func<PropertyFieldInfo, Dictionary<string, object>, object> pFactory, PropertyFieldInfo pfi)
        {
            if (!pfi.HasInjectAttribute())
                return;

            Dictionary<string, object> defaultValues = null;
            if (pfi.HasDefaultValuesAttribute())
                defaultValues = GetDefaultValues(pServerPlugin, pfi.GetDefaultValuesAttribute().MethodName);

            if (!pfi.HasStorageLocationAttribute())
                throw new Exception($"No LocationAttribute defined for Storage {pfi.GetName()}");

            pfi.SetValue(null, pFactory.Invoke(pfi, defaultValues));
        }

        private static Dictionary<string, object> GetDefaultValues(Type pServerPlugin, string pMethodName)
        {
            MethodInfo mi = pServerPlugin.GetMethod(pMethodName);

            if (mi == null)
                throw new Exception($"{pServerPlugin} does not implement a public method '{pMethodName}'");

            if (!mi.IsStatic)
                throw new Exception($"{pServerPlugin}.{pMethodName}' needs to be static");

            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];

            if (configs == null)
                throw new Exception($"{pServerPlugin}.{pMethodName} does not have the corrent return type {nameof(KeyDefaultValue)}[] ");

            return configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue);
        }

        public static string GetServerPluginFolder(Type pServerPlugin)
        {
            string folder = pServerPlugin.ToString();

            AsphaltPluginAttribute att = ((AsphaltPluginAttribute)pServerPlugin.GetCustomAttribute(typeof(AsphaltPluginAttribute)));
            if (IsAsphaltPlugin(pServerPlugin) && att.ModName != null)
                folder = att.ModName;

            if (folder.Contains("."))
                folder = folder.Substring(folder.IndexOf(".") + 1);

            return Path.Combine("Mods", folder);
        }

        public static bool IsAsphaltPlugin(Type pType)
        {
            return pType.GetCustomAttribute(typeof(AsphaltPluginAttribute)) != null;
        }
    }
}
