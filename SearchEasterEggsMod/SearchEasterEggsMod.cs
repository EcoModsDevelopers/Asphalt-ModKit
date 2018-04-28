using Eco.Core.Plugins.Interfaces;
using Asphalt.Service;
using Asphalt.Storeable;
using Asphalt;
using Asphalt.Service.Permissions;
using System;

namespace SearchEasterEggsMod
{
    public class SearchEasterEggsMod : IModKitPlugin
    {
        [Inject]
        public IStorage ConfigStorage { get; set; }

        [Inject]
        public IStorageCollection SettingsCollection { get; set; }

        [Inject]
        public IPermissionChecker PermissionChecker { get; set; }

        //   public IStorage<EasterEggConfigValues> ConfigStorage2 { get; set; }

        [Inject]
        [StorageName("customConfig")]
        public IStorage CustomConfig { get; set; }


        public SearchEasterEggsMod()
        {
            // ConfigStorage is null here!
        }

        public void OnEnable()
        {
            // SettingsCollection.setDirectory("dfg");

            try
            {
                int maximumEggsInWorld = ConfigStorage.GetInt("MaximumEggsInWorld");

                //    int? maximumEggsInWorld2 = ConfigStorage.Get<int?>("MaximumEggsInWorld");

                /*
                                IStorage hStorage = SettingsCollection.GetStorage("hansi");

                                hStorage.GetInt("CollectedEggs");

                                PermissionChecker.CheckPermission(null, "ff");*/

                //   int maximumEggsInWorld2 = ConfigStorage2.GetInt(EasterEggConfigValues.MaximumEggsInWorld);


                //   Uri test = ConfigStorage2.Get<Uri>(EasterEggConfigValues.MaximumEggsInWorld);
            }
            catch (Exception e)
            {

            }

        }

        public KeyDefaultValue[] GetConfig()
        {
            return new KeyDefaultValue[]
            {
                new KeyDefaultValue("MaximumEggsInWorld","42"),
            };
        }


        public string GetStatus()
        {
            return "";
        }
    }
}
