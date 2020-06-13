using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Event.RpcEvents;
using Asphalt.Events.Console;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.WorldObjectEvent;
using Eco.Shared.Items;
using EcoTestEventPlugin.util;
using System;
using System.ComponentModel;
using System.Text;

namespace EcoTestEventPlugin
{
    public class TestEventHandlers
    {

        // Inventory Events

        
        [EventHandler]
        public void InventoryChangeSelectedSlotEvent(InventoryChangeSelectedSlotEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void InventoryMoveItemEvent(InventoryMoveItemEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        // Player Events

        [EventHandler]
        public void PlayerBuyEvent(PlayerBuyEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerClaimPropertyEvent(PlayerClaimPropertyEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerCompleteContractEvent(PlayerCompleteContractEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerCraftEvent(PlayerCraftEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerEatEvent(PlayerEatEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }
       
        [EventHandler]
        public void PlayerGainSkillEvent(PlayerGainSkillEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerGetElectedEvent(PlayerGetElectedEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerHarvestEvent(PlayerHarvestEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerInteractEvent(PlayerInteractEvent evt)
        {
            if (evt.Context.Method.Equals(InteractionMethod.None))
                return; // avoid spam
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerLoginEvent(PlayerLoginEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerLogoutEvent(PlayerLogoutEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerPayTaxEvent(PlayerPayTaxEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerPickUpEvent(PlayerPickUpEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerPlaceEvent(PlayerPlaceEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerProposeVoteEvent(PlayerProposeVoteEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerReceiveGovernmentFundsEvent(PlayerReceiveGovernmentFundsEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerRunForElectionEvent(PlayerRunForElectionEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerSellEvent(PlayerSellEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerSendMessageEvent(PlayerSendMessageEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerUnlearnSkillEvent(PlayerUnlearnSkillEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void PlayerVoteEvent(PlayerVoteEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        // RPC Events

        [EventHandler]
        public void RpcInvokeEvent(RpcInvokeEvent evt)
        {
            // Console.WriteLine(EventUtil.EventToString(evt)); // Avoid spam
        }

        // World Events

        [EventHandler]
        public void WorldPolluteEvent(WorldPolluteEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt)); // avoid spam
        }

        // WorldObject Events

        [EventHandler]
        public void RubbleSpawnEvent(RubbleSpawnEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void TreeFellEvent(TreeFellEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void TreeChopEvent(TreeChopEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }


        [EventHandler]
        public void WorldObjectChangeTextEvent(WorldObjectChangeTextEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void WorldObjectDestroyedEvent(WorldObjectDestroyedEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void WorldObjectEnabledChangedEvent(WorldObjectEnabledChangedEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void WorldObjectNameChangedEvent(WorldObjectNameChangedEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void WorldObjectOperatingChangedEvent(WorldObjectOperatingChangedEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }

        [EventHandler]
        public void WorldObjectPickupEvent(WorldObjectPickupEvent evt)
        {
            Console.WriteLine(EventUtil.EventToString(evt));
        }


    }
}
