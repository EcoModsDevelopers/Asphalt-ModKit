using Asphalt.Api.Util;
using Asphalt.Service;
using Asphalt.Service.Permissions;
using Asphalt.Storeable;
using Asphalt.Storeable.Json;
using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Asphalt.Util
{
    public class ServiceHelper
    {
        internal static void CallMethod(string pName)
        {
            Eco.Core.PluginManager.Controller.ForEach(s => CallMethod(s, pName));
        }

        private static void CallMethod(IServerPlugin pServerPlugin, string pName)
        {
            MethodInfo mi = pServerPlugin.GetType().GetMethod(pName);

            if (mi == null || !Injection.HasInjectAttribute(mi))
                return;

            mi.Invoke(pServerPlugin, new object[] { });
        }

        internal static void InjectValues()
        {
            Eco.Core.PluginManager.Controller.ForEach(s => InjectValues(s));
        }

        private static void InjectValues(IServerPlugin pServerPlugin)
        {
            InjectPermissions(pServerPlugin);

            foreach(PropertyFieldInfo pfi in GetPropertyFieldInfos(pServerPlugin, typeof(IStorage)))
                Inject(pServerPlugin, (l_pfi, defaultValues) => new JsonFileStorage(Path.Combine(GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues, true), pfi);

            foreach (PropertyFieldInfo pfi in GetPropertyFieldInfos(pServerPlugin, typeof(IStorageCollection)))
                Inject(pServerPlugin, (l_pfi, defaultValues) => new JsonFileStorageCollection(Path.Combine(GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues), pfi);

            foreach (PropertyFieldInfo pfi in GetPropertyFieldInfos(pServerPlugin, typeof(IUserStorageCollection)))
                Inject(pServerPlugin, (l_pfi, defaultValues) => new JsonFileUserStorageCollection(Path.Combine(GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues), pfi);
        }

        private static void InjectPermissions(IServerPlugin pServerPlugin)
        {
            PropertyInfo pi = pServerPlugin.GetType().GetProperty("PermissionService");
            FieldInfo fi = pServerPlugin.GetType().GetField("PermissionService");

            if (!(pi != null && Injection.HasInjectAttribute(pi)) &&
                !(fi != null && Injection.HasInjectAttribute(fi)))
                return;

            MethodInfo mi = pServerPlugin.GetType().GetMethod("GetDefaultPermissions");

            if (mi == null)
                throw new Exception($"{pServerPlugin.GetType()} does not implement a public method GetDefaultPermission()");

            object permissionList = mi.Invoke(pServerPlugin, new object[] { });

            DefaultPermission[] permissions = permissionList as DefaultPermission[];

            if (permissions == null)
                throw new Exception($"{pServerPlugin.GetType()}.GetDefaultPermission() does not have the corrent return type {nameof(DefaultPermission)}[] ");

            JsonFilePermissionStorage storage = new JsonFilePermissionStorage(Path.Combine(GetServerPluginFolder(pServerPlugin), "permissions.json"), permissions.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));

            pi?.SetValue(pServerPlugin, storage);
            fi?.SetValue(pServerPlugin, storage);
        }

        private static void Inject(IServerPlugin pServerPlugin, Func<PropertyFieldInfo, Dictionary<string, object>, object> pFactory, PropertyFieldInfo pfi)
        {
            if (!pfi.HasInjectAttribute())
                return;

            Dictionary<string, object> defaultValues = null;
            if (pfi.HasDefaultValuesAttribute())
                defaultValues = GetDefaultValues(pServerPlugin, pfi.GetDefaultValuesAttribute().MethodName);

            if (!pfi.HasStorageLocationAttribute())
                throw new Exception($"No LocationAttribute defined for Storage {pfi.GetName()}");

            pfi.SetValue(pServerPlugin, pFactory.Invoke(pfi, defaultValues));
        }

        private static Dictionary<string, object> GetDefaultValues(IServerPlugin pServerPlugin, string pMethodName)
        {
            MethodInfo mi = pServerPlugin.GetType().GetMethod(pMethodName);

            if (mi == null)
                throw new Exception($"{pServerPlugin.GetType()} does not implement a public method '{pMethodName}'");

            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];

            if (configs == null)
                throw new Exception($"{pServerPlugin.GetType()}.{pMethodName} does not have the corrent return type {nameof(KeyDefaultValue)}[] ");

            return configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue);
        }

        public static string GetServerPluginFolder(IServerPlugin pServerPlugin)
        {
            string folder = pServerPlugin.ToString();

            if (folder.Contains("."))
                folder = folder.Substring(folder.IndexOf(".") + 1);

            return Path.Combine("Mods", folder);
        }

        public static IEnumerable<PropertyFieldInfo> GetPropertyFieldInfos(IServerPlugin pServerPlugin, Type pType)
        {
            return pServerPlugin.GetType().GetProperties().Where(x => x.PropertyType == pType).Select(x => new PropertyFieldInfo(x)).Concat(pServerPlugin.GetType().GetFields().Where(x => x.FieldType == pType).Select(x => new PropertyFieldInfo(x)));
        }
    }
}
