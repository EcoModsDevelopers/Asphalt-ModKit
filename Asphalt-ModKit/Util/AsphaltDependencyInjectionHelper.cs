using Asphalt.Service;
using Asphalt.Service.Config;
using Asphalt.Storeable;
using Asphalt.Storeable.JSON;
using Eco.Core.Plugins.Interfaces;
using Eco.Server;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            CustomConfigFile storage = new CustomConfigFile(pServerPlugin.ToString());


            //public KeyDefaultValue<string>[] GetConfig()

            MethodInfo mi = pServerPlugin.GetType().GetMethod("GetConfig");
            object configList = mi.Invoke(pServerPlugin, new object[] { });

            KeyDefaultValue[] configs = configList as KeyDefaultValue[];
                  
            storage.Reload();
            storage.SetValues(configs.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue));
            storage.Save();

            pi.SetValue(pServerPlugin, storage);

            pServerPlugin.GetType().GetMethod("OnEnable")?.Invoke(pServerPlugin, new object[] { });
        }



    }
}
