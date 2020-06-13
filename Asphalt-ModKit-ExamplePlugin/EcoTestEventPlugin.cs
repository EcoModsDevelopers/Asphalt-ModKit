using Eco.Core.Plugins.Interfaces;
using Asphalt.Api.Event;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Components;
using System;
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using Asphalt.Utils;

namespace EcoTestEventPlugin
{
    public class EcoTestEventPlugin : IModKitPlugin, IServerPlugin
    {
        public static TestEventListener TestListener { get; protected set; }

        public EcoTestEventPlugin()
        {
            TestListener = new TestEventListener();

            EventManager.RegisterListener(TestListener);
        }

        public string GetStatus()
        {
            return "Test Asphalt Events and stuff";
        }

        public override string ToString()
        {
            return "EcoTestEventPlugin";
        }
    }
}
