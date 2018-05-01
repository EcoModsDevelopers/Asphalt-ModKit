using Asphalt.Api.Util;
using Asphalt.Service;
using Asphalt.Service.Permissions;
using Asphalt.Storeable;
using Asphalt.Storeable.Json;
using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using System;
using System.IO;
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
            InjectConfig(pServerPlugin);
            InjectPermissions(pServerPlugin);
        }

        private static void InjectConfig(IServerPlugin pServerPlugin)
        {
            PropertyInfo pi = pServerPlugin.GetType().GetProperty("ConfigStorage");
            FieldInfo fi = pServerPlugin.GetType().GetField("ConfigStorage");

            if (!(pi != null && Injection.HasInjectAttribute(pi)) &&
                !(fi != null && Injection.HasInjectAttribute(fi)))
                return;

            //public KeyDefaultValue<string>[] GetConfig()

            MethodInfo mi = pServerPlugin.GetType().GetMethod("GetConfig");

            if (mi == null)
                throw new Exception($"{pServerPlugin.GetType()} does not implement a public method GetConfig()");

            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];

            if (configs == null)
                throw new Exception($"{pServerPlugin.GetType()}.GetConfig() does not have the corrent return type {nameof(KeyDefaultValue)}[] ");

            JsonFileStorage storage = new JsonFileStorage(Path.Combine(GetServerPluginFolder(pServerPlugin), "config.json"));
            storage.MergeWithDefaultValues(configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));
            storage.ForceSave();

            pi?.SetValue(pServerPlugin, storage);
            fi?.SetValue(pServerPlugin, storage);
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

            JsonFilePermissionStorage storage = new JsonFilePermissionStorage(Path.Combine(GetServerPluginFolder(pServerPlugin), "permissions.json"));
            storage.MergeWithDefaultValues(permissions.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));
            storage.ForceSave();

            pi?.SetValue(pServerPlugin, storage);
            fi?.SetValue(pServerPlugin, storage);
        }

        private static string GetServerPluginFolder(IServerPlugin pServerPlugin)
        {
            string folder = pServerPlugin.ToString();

            if (folder.Contains("."))
                folder = folder.Substring(folder.IndexOf(".") + 1);

            return Path.Combine("Mods", folder);
        }
    }
}
