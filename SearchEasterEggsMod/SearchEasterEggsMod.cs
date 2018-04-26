using Eco.Core.Plugins.Interfaces;
using Asphalt.Service;
using Asphalt.Storeable;
using Asphalt;
using Asphalt.Service.Permissions;

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

            int? maximumEggsInWorld = ConfigStorage.GetInt("MaximumEggsInWorld");
            int maximumEggsInWorld2 = ConfigStorage.Get<int>("MaximumEggsInWorld");


            IStorage hStorage = SettingsCollection.GetStorage("hansi");

            hStorage.GetInt("CollectedEggs");

            PermissionChecker.CheckPermission(null, "ff");

            //   int maximumEggsInWorld2 = ConfigStorage2.GetInt(EasterEggConfigValues.MaximumEggsInWorld);


            //   Uri test = ConfigStorage2.Get<Uri>(EasterEggConfigValues.MaximumEggsInWorld);

        }



        public KeyDefaultValue[] GetConfig()
        {
            return new KeyDefaultValue[]
            {
                new KeyDefaultValue("MaximumEggsInWorld","42"),
            };
        }

        /*
        public (string, string)[] GetConfig()
        {
            return new(string Key, string DefaultValue)[]
            {
                ("MaximumEggsInWorld", "1000"),
                ("EggsToFindForReward", "10")
            };
        }

        public (EasterEggConfigValues, string)[] GetConfig2()
        {
            return new(EasterEggConfigValues Key, string DefaultValue)[]
            {
                (EasterEggConfigValues.MaximumEggsInWorld, "1000"),
                (EasterEggConfigValues.EggsToFindForReward, "10")
            };
        }
        */

        public string GetStatus()
        {
            return "";
        }
    }
}
