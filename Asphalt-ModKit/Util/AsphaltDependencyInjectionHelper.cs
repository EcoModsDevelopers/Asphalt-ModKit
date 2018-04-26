using Asphalt.Service;
using Asphalt.Storeable.Json;
using Eco.Core.Plugins.Interfaces;
using Eco.Server;
using Eco.Shared.Utils;
using System.Reflection;

namespace Asphalt.Util
{
    public class AsphaltDependencyInjectionHelper
    {

        public static void init()
        {
            PluginManager.OnPluginAdd = (s) => InjectValues(s); //Harmony Prefix

            //Loop through PluginManager.plugin because we have maybe bad luck and we aren't the first plugin which is loaded
        }


        public static void InjectValues(IServerPlugin pServerPlugin)
        {
            PropertyInfo pi = pServerPlugin.GetType().GetProperty("ConfigStorage");

            if (pi == null)
                return;

            JsonFileStorage storage = new JsonFileStorage(pServerPlugin.ToString(), configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));
            
            //public KeyDefaultValue<string>[] GetConfig()

            MethodInfo mi = pServerPlugin.GetType().GetMethod("GetConfig");
            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];

            storage.Reload();
            storage.SetValues();
            storage.Save();

            pi.SetValue(pServerPlugin, storage);

            pServerPlugin.GetType().GetMethod("OnEnable")?.Invoke(pServerPlugin, new object[] { });
        }



    }
}
