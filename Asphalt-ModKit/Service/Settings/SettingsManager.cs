using Eco.Core.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asphalt.Service.Settings
{
    /*
    public class SettingsManager
    {
        private AsphaltMod mod;
        private Type type;
        public string Name { get; private set; }

        private CustomSettingsFile defaultFile;
        private Dictionary<string, CustomSettingsFile> userSettings;

        public SettingsManager(AsphaltMod mod, Type type)
        {
            this.mod = mod;
            this.type = type;

            this.Name = InitDefault();
            LoadSettings();
        }

        private string InitDefault()
        {
            try
            {
                CustomSettingsFile settings = CreateSettings(this.mod, this.type, null);
                this.defaultFile = RegisterSettings(settings);

                return settings.GetSettingsName();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        private void LoadSettings()
        {
            this.userSettings = new Dictionary<string, CustomSettingsFile>();

            string[] files = Directory.GetFiles(Path.GetDirectoryName(this.defaultFile.GetFilename()));

            if (files == null || files.Length < 1)
                return;

            foreach (string path in files)
            {
                string file = Path.GetFileName(path);

                if (file.Equals("_default.json", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                try
                {
                    CustomSettingsFile settings = CreateSettings(this.mod, this.type, file);
                    settings = RegisterSettings(settings);

                    string slgId = file.Replace(".json", "");

                    if (this.userSettings.ContainsKey(slgId))
                        this.userSettings.Remove(slgId);
                    this.userSettings.Add(slgId, settings);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private CustomSettingsFile CreateSettings(IServerPlugin plugin, Type type, string customName)
        {
            try
            {
                if (customName == null)
                    return (CustomSettingsFile)Activator.CreateInstance(type, plugin);
                else
                    return (CustomSettingsFile)Activator.CreateInstance(type, plugin, customName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private CustomSettingsFile RegisterSettings(CustomSettingsFile settings)
        {
            settings.Reload();
            return settings;
        }

        public void Reload()
        {
            InitDefault();

            foreach (KeyValuePair<string, CustomSettingsFile> pair in userSettings)
            {
                pair.Value.Reload();
            }
        }

        public CustomSettingsFile GetDefaultFile()
        {
            return this.defaultFile;
        }

        public CustomSettingsFile GetSettings(string slgId)
        {
            if (!this.userSettings.ContainsKey(slgId))
                return null;

            return this.userSettings[slgId];
        }

        public CustomSettingsFile CreateSettingsFileForUser(string slgId)
        {
            if (this.userSettings.ContainsKey(slgId))
                return this.userSettings[slgId];

            try
            {
                CustomSettingsFile settings = CreateSettings(this.mod, this.type, slgId + ".json");

                if (settings == null)
                    return null;

                this.userSettings.Add(slgId, settings);

                return RegisterSettings(settings);
            }
            catch
            {
                return null;
            }
        }
    } */
}
