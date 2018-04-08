using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoTestEventPlugin
{
    public class TestEventHandlers
    {
        [EventHandler(EventPriority.Normal, RunIfEventCancelled = false)]
        public void OnPlayerMessage(PlayerSendMessageEvent evt)
        {
            //  evt.SetCancelled(true);
            Console.WriteLine(evt.Message.Text);
        }

        [EventHandler]
        public void OnPlayerInteract(PlayerInteractEvent evt)
        {
            //       evt.SetCancelled(true);
            Console.WriteLine(evt.Context.CarriedItem);
            Console.WriteLine(evt.Context.Player.FriendlyName);
        }

        [EventHandler]
        public void OnPlayerJoin(PlayerLoginEvent evt)
        {
            Console.WriteLine("Hello " + evt.Player.FriendlyName);
        }

        [EventHandler]
        public void OnPlayerLogout(PlayerLogoutEvent evt)
        {
            Console.WriteLine("Bye " + evt.User.Name);
        }

        [EventHandler]
        public void OnPlayerCraft(PlayerCraftEvent evt)
        {
            Console.WriteLine("craft " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Table);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerCraft(WorldPolluteEvent evt)
        {

         //   Console.WriteLine(evt.Component);
         //   Console.WriteLine(evt.User);
         //   Console.WriteLine(evt.Value);
            //    evt.SetCancelled(true);
            //     evt.Value = 1;
        }
    }
}
