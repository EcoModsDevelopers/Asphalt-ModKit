using Asphalt.Api.Util;
using Asphalt.Service;
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
            MethodInfo pi = pServerPlugin.GetType().GetMethod(pName);
            if (pi == null)
                return;
            pi.Invoke(pServerPlugin, new object[] { });
        }

        internal static void InjectValues()
        {
            Eco.Core.PluginManager.Controller.ForEach(s => InjectValues(s));
        }

        private static void InjectValues(IServerPlugin pServerPlugin)
        {
            InjectConfig(pServerPlugin);
        }

        private static void InjectConfig(IServerPlugin pServerPlugin)
        {
            PropertyInfo pi = pServerPlugin.GetType().GetProperty("ConfigStorage");

            if (pi == null || !Injection.HasInjectAttribute(pi))
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

            pi.SetValue(pServerPlugin, storage);
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
