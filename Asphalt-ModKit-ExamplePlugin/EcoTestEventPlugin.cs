using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Stats;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Core.Utils.AtomicAction;
using Eco.Core.Utils;
using Eco.Shared.Localization;
using System.Reflection;
using Eco.Stats;
using Eco.Gameplay.Interactions;
using Eco.Shared.Items;
using Eco.Gameplay.Players;
using Eco.World;
using Asphalt.Api.Event;

namespace EcoTestEventPlugin
{
    public class EcoTestEventPlugin : IModKitPlugin, IServerPlugin
    {
        public EcoTestEventPlugin()
        {
            EventManager.RegisterListener(new TestEventHandlers());
        }

        public string GetStatus()
        {
            return "";
        }

        public override string ToString()
        {
            return "EcoTestEventPlugin";
        }
    }
}
