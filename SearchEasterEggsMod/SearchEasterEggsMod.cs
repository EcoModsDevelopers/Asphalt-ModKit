using Eco.Core.Plugins.Interfaces;
using Asphalt.Service;
using Asphalt.Storeable;
using Asphalt;
using Asphalt.Service.Permissions;
using System;

namespace SearchEasterEggsMod
{
    [AsphaltPlugin]
    public class SearchEasterEggsMod : IModKitPlugin
    {
        [Inject]
        [StorageLocation("Config")]
        [DefaultValues(nameof(GetConfig))]
        public static IStorage ConfigStorage { get; set; }

        [Inject]
        [StorageLocation("CollectedEggsStorage")]
        public static IUserStorageCollection CollectedEggsStorage { get; set; }

        [Inject]
        public static IPermissionService PermissionChecker { get; set; }

        public SearchEasterEggsMod()
        {
            // Properties are null here. Do not use contructor if you are using Asphalt
        }

        public void OnEnable()
        {
            int maximumEggsInWorld = ConfigStorage.GetInt("MaximumEggsInWorld");
        }

        public KeyDefaultValue[] GetConfig()
        {
            return new KeyDefaultValue[]
            {
                new KeyDefaultValue("MaximumEggsInWorld","42"),
            };
        }

        public DefaultPermission[] GetDefaultPermissions()
        {
            return new DefaultPermission[]
            {
                new DefaultPermission("easteregg.collect", PermissionGroup.User),
                new DefaultPermission("easteregg.create", PermissionGroup.User)
            };
        }

        public string GetStatus()
        {
            return "Unknown";
        }
    }
}
