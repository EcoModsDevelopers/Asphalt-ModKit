using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Event.RpcEvents;
using Asphalt.Events;
using Asphalt.Events.InventoryEvents;
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
        public void OnPlayerTeleport(PlayerTeleportEvent evt)
        {
            Console.WriteLine("Teleport " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Position);
            //evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerLogout(PlayerLogoutEvent evt)
        {
            Console.WriteLine("Bye " + evt.User.Name);
        }

        [EventHandler]
        public void OnPlayerCraft(PlayerCraftEvent evt)
        {
            Console.WriteLine("Craft " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Table);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerGainSkill(PlayerGainSkillEvent evt)
        {
            Console.WriteLine("GainSkill " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Skill.FriendlyName);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerUnlearnSkill(PlayerUnlearnSkillEvent evt)
        {
            Console.WriteLine("UnlearnSkill " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Skill.FriendlyName);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerClaimProperty(PlayerClaimPropertyEvent evt)
        {
            Console.WriteLine("ClaimProperty " + evt.Player.FriendlyName);
            Console.WriteLine(evt.Position);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnWorldObjectDestroyed(WorldObjectDestroyedEvent evt)
        {
            Console.WriteLine("Destroyed"+evt.WorldObject.ToString());
        }

        [EventHandler]
        public void OnWorldObjectEnabled(WorldObjectEnabledChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
            //   Console.WriteLine(evt.User);
            //   Console.WriteLine(evt.Value);
            //    evt.SetCancelled(true);
            //     evt.Value = 1;
        }

        [EventHandler]
        public void OnWorldObjectNameChanged(WorldObjectNameChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
        }

        [EventHandler]
        public void OnWorldObjectOperatingChanged(WorldObjectOperatingChangedEvent evt)
        {
            Console.WriteLine(evt.WorldObject.ToString());
        }

        /*
        [EventHandler]
        public void OnWorldPollute(WorldPolluteEvent evt)
        {
            evt.SetCancelled(true);
            Console.WriteLine(evt.User.ToString());
        }*/


        [EventHandler]
        public void OnPlayerEatEvent(PlayerEatEvent evt)
        {
            Console.WriteLine(evt.FoodItem);
        }

        [EventHandler]
        public void OnInventoryMoveItemEvent(InventoryMoveItemEvent evt)
        {
            Console.Write($"RpcInvokeEvent: ");
            if (evt.User != null) Console.Write($"{evt.User.Name}");
            Console.Write($" moved from {evt.SourceStack.Quantity}x {evt.SourceStack.Item.FriendlyName}");
            if (!evt.DestinationStack.Empty) Console.Write($" to {evt.DestinationStack.Quantity}x {evt.DestinationStack.Item.FriendlyName}");
            Console.WriteLine("");

            //evt.SetCancelled(true);
        }


    }
}
