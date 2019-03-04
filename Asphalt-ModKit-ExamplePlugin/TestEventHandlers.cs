using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Event.RpcEvents;
using Asphalt.Events.Console;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.WorldObjectEvent;
using System;

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
            Console.WriteLine(evt.Context.CarriedItem);
            Console.WriteLine(evt.Context.Player.DisplayName);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerJoin(PlayerLoginEvent evt)
        {
            Console.WriteLine("Hello " + evt.Player.DisplayName);
        }

        [EventHandler]
        public void OnPlayerTeleport(PlayerTeleportEvent evt)
        {
            Console.WriteLine("Teleport " + evt.Player.DisplayName);
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
            Console.WriteLine("Craft " + evt.User.Player.DisplayName);
            Console.WriteLine(evt.Table);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerGainSkill(PlayerGainSkillEvent evt)
        {
            Console.WriteLine("GainSkill " + evt.Player.DisplayName);
            Console.WriteLine(evt.Skill.DisplayName);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerUnlearnSkill(PlayerUnlearnSkillEvent evt)
        {
            Console.WriteLine("UnlearnSkill " + evt.Player.DisplayName);
            Console.WriteLine(evt.Skill.DisplayName);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnPlayerClaimProperty(PlayerClaimPropertyEvent evt)
        {
            Console.WriteLine("ClaimProperty " + evt.User.Player.DisplayName);
            Console.WriteLine(evt.Position);
            // evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnWorldObjectDestroyed(WorldObjectDestroyedEvent evt)
        {
            Console.WriteLine("Destroyed" + evt.WorldObject.ToString());
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
        public void OnPlayerSendMessageEvent(PlayerSendMessageEvent evt)
        {
            Console.WriteLine($"{evt.User.Name} sent message '{evt.Message}'");
            evt.Message.Text = "Test";
        }

        [EventHandler]
        public void OnInventoryMoveItemEvent(InventoryMoveItemEvent evt)
        {
            Console.Write($"InventoryMoveItemEvent: ");
            if (evt.User != null) Console.Write($"{evt.User.Name}");
            Console.Write($" moved from {evt.SourceStack.Quantity}x {evt.SourceStack.Item.DisplayName}");
            if (!evt.DestinationStack.Empty) Console.Write($" to {evt.DestinationStack.Quantity}x {evt.DestinationStack.Item.DisplayName}");
            Console.WriteLine("");

            //evt.SetCancelled(true);
        }


        [EventHandler]
        public void OnInventoryChangeSelectedSlotEvent(InventoryChangeSelectedSlotEvent evt)
        {
            Console.Write($"InventoryChangeSelectedSlotEvent: {evt.Player.DisplayName} changed to slot {evt.SelectedSlot}");
            if (!evt.SelectedStack.Empty) Console.Write($" with {evt.SelectedStack.Quantity}x {evt.SelectedStack.Item.DisplayName}");
            Console.WriteLine("");
        }


        [EventHandler]
        public void OnSignChangeEvent(WorldObjectChangeTextEvent evt)
        {
            Console.WriteLine($"SignChangeEvent: {evt.Player.DisplayName} set text to {evt.Text}");
        }

        [EventHandler]
        public void OnSpawnRubbleEvent(RubbleSpawnEvent evt)
        {
            Console.WriteLine($"SpawnRubbleEvent: spawned {evt.RubbleObject.GetType().ToString()} at {evt.RubbleObject.Position.ToString()}");
            //if (evt.RubbleObject.IsBreakable)
            //    evt.RubbleObject.Breakup();
            //evt.SetCancelled(true);
        }

        [EventHandler]
        public void OnSpawnRubbleEvent(RpcInvokeEvent evt)
        {
            Console.WriteLine($"rpc received {evt.Methodname}");
        }

        [EventHandler]
        public void OnConsoleInput(ConsoleInputEvent evt)
        {
            Console.WriteLine("Console Input: " + evt.Text);
        }
    }
}
