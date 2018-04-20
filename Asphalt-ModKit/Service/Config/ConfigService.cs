using System.Collections.Generic;

namespace Asphalt.Service.Config
{
    public class ConfigService : AbstractService
    {
        private CustomConfigFile file;

        private Dictionary<string, object> configFiels;

        public ConfigService(AsphaltMod mod) : base(mod) { }

        public override void Init()
        {
            file = new CustomConfigFile(Mod);
            configFiels = new Dictionary<string, object>();

            LoadDefaultPermissions();
            LoadPermissionsFromFile();
        }

        public override void Reload()
        {
            configFiels.Clear();

            LoadDefaultPermissions();
            LoadPermissionsFromFile();
        }

        private void LoadDefaultPermissions()
        {
            foreach(ConfigField field in Mod.GetConfigFields())
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

        public CustomConfigFile GetConfig()
        {
            return file;
        }
    }
}
