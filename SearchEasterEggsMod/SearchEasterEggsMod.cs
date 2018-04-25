using Asphalt.Service.Config;
using Eco.Core.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asphalt.Service;
using Asphalt.Storeable;
using Eco.ModKit;

namespace SearchEasterEggsMod
{
    public class SearchEasterEggsMod : IModKitPlugin
    {

        public IStorage ConfigStorage { get; set; }

        public IStorageCollection SettingsCollection { get; set; }

        //   public IStorage<EasterEggConfigValues> ConfigStorage2 { get; set; }


        public SearchEasterEggsMod()
        {
            // ConfigStorage is null here!
        }

        public void OnEnable()
        {
            // SettingsCollection.setDirectory("dfg");

            int maximumEggsInWorld = ConfigStorage.GetInt("MaximumEggsInWorld");


            IStorage hStorage = SettingsCollection.GetStorage("hansi");

            hStorage.GetInt("CollectedEggs");



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
