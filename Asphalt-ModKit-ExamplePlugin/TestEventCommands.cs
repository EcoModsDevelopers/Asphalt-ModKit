using Asphalt.Api.Event;
using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoTestEventPlugin
{
    public class TestEventCommands: IChatCommandHandler
    {

        [ChatCommand("registerevents", "Registers all Asphalt TestEvents", ChatAuthorizationLevel.Admin)]
        public static void RegisterTestEvents()
        {
            EventManager.RegisterListener(EcoTestEventPlugin.TestListener);
        }

        [ChatCommand("unregisterevents", "Unregisters all Asphalt TestEvents", ChatAuthorizationLevel.Admin)]
        public static void UnregisterTestEvents()
        {
     //       EventManager.UnregisterListener(EcoTestEventPlugin.TestListener);
        }
    }
}
