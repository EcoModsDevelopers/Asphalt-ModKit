using Asphalt.AsphaltExceptions;
using Eco.Gameplay.Players;
using System;
using System.Collections.Generic;

namespace Asphalt.Service.Settings
{
    /*
    public class SettingsService : IAspahltService
    {

        public static readonly string SETTINGS_DIR = "settings";

        private Dictionary<Type, SettingsManager> settingsManagers;

        public SettingsService(AsphaltMod mod) : base(mod) { }

        public override void Init()
        {
            this.LoadSettings();
        }

        public override void Reload()
        {
            foreach (KeyValuePair<Type, SettingsManager> pair in settingsManagers)
            {
                pair.Value.Reload();
            }
        }

        private void LoadSettings()
        {
            settingsManagers = new Dictionary<Type, SettingsManager>();

            //add all Settings
            if (this.Mod.GetCustomSettings() == null)
                return;

            foreach(Type type in this.Mod.GetCustomSettings())
            {
                try
                {
                    SettingsManager manager = new SettingsManager(this.Mod, type);
                    settingsManagers.Add(type, manager);
                }
                catch (ServiceInitException e)
                {
                    //TODO print stacktrace
                }
            }
        }

        
        //  external getters SettingsManager
        

        public SettingsManager GetSettingsManager(Type type)
        {
            return this.settingsManagers[type];
        }

        
        // external getters Settings
         

        //READ-AND-WRITE

        public CustomSettingsFile GetSettings(Type type, string slgId)
        {
            SettingsManager manager = settingsManagers[type];
            return GetOrCreateSettingsForUser(manager, slgId);
        }

        public CustomSettingsFile GetSettings(Type type, User user)
        {
            return GetSettings(type, user.SlgId);
        }

        public CustomSettingsFile GetSettings(Type type, Player player)
        {
            return GetSettings(type, player.User.SlgId);
        }

        //READ-ONLY

        public CustomSettingsFile GetReadOnlySettings(Type type, string slgId)
        {
            SettingsManager manager = settingsManagers[type];
            return GetSettingsForUser(manager, slgId);
        }

        public CustomSettingsFile GetReadOnlySettings(Type type, User user)
        {
            return GetReadOnlySettings(type, user.SlgId);
        }

        public CustomSettingsFile GetReadOnlySettings(Type type, Player player)
        {
            return GetReadOnlySettings(type, player.User.SlgId);
        }

       
        // internal getters Settings
       

        private CustomSettingsFile GetOrCreateSettingsForUser(SettingsManager manager, string slgId)
        {
            if (manager == null)
                return null;

            CustomSettingsFile settings = manager.GetSettings(slgId);
            if (settings == null)
                settings = manager.CreateSettingsFileForUser(slgId);

            return settings;
        }

        private CustomSettingsFile GetSettingsForUser(SettingsManager manager, string slgId)
        {
            if (manager == null)
                return null;

            CustomSettingsFile settings = manager.GetSettings(slgId);
            if (settings == null)
                settings = manager.GetDefaultFile();

            return settings;
        }
        
}*/
}
