using Asphalt.Storeable;
using System.Collections.Generic;

namespace Asphalt.Service.Config
{/*
    public class ConfigService : IAspahltService
    {
        private CustomConfigFile file;

        private Dictionary<string, object> configFiels;

        public void Init()
        {
            file = new CustomConfigFile();
            Reload();
        }

        public void Reload()
        {
            configFiels = new Dictionary<string, object>();

            LoadDefaultPermissions();
            LoadPermissionsFromFile();
        }

        private void LoadDefaultPermissions()
        {
            foreach (KeyDefaultValue field in Mod.GetConfigFields())
            {
                if (configFiels.ContainsKey(field.Key))
                    configFiels.Remove(field.Key);

                configFiels.Add(field.Key, field.DefaultValue);
            }
        }

        private void LoadPermissionsFromFile()
        {
            file.Reload();

            foreach (KeyValuePair<string, object> pair in file.GetValues())
            {
                try
                {
                    configFiels[pair.Key] = pair.Value;
                }
                catch
                {
                    //TODO: maybe log smth
                }
            }

            file.SetValues(configFiels);
            file.Save();
        }

        public IStorage GetConfig()
        {
            return file;
        }
    }*/
}
