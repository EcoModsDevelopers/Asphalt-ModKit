using Asphalt.Service;
using Asphalt.Storeable;
using Asphalt.Storeable.Json;
using Eco.Core.Plugins.Interfaces;
using Eco.Server;
using Eco.Shared.Utils;
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
            PropertyInfo pi = pServerPlugin.GetType().GetProperty("ConfigStorage");

            if (pi == null)
                return;
            
            //public KeyDefaultValue<string>[] GetConfig()

            MethodInfo mi = pServerPlugin.GetType().GetMethod("GetConfig");
            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];

            MemoryStorage memStorage = new MemoryStorage(configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));

            JsonFileStorage storage = new JsonFileStorage(pServerPlugin.ToString(), memStorage);

            storage.Reload();

            storage.Save();

            pi.SetValue(pServerPlugin, storage);
        }
    }
}
