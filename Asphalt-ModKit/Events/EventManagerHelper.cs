using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Event.RpcEvents;
using Asphalt.Api.Util;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.WorldObjectEvent;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Shared.Networking;
using System;
using System.Linq;

namespace Asphalt.Events
{
    internal static class EventManagerHelper
    {
        internal static void injectEvent(Type pEventType)
        {
            switch (pEventType.Name) //We hope Event names are unique
            {

                // Inventory Events

                case nameof(InventoryChangeSelectedSlotEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(SelectionInventory), typeof(InventoryChangeSelectedSlotEventHelper), "SelectIndex");
                    break;
                case nameof(InventoryMoveItemEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(InventoryChangeSet), typeof(InventoryMoveItemEventHelper), "MoveStacks");
                    break;

                // Player Events

                case nameof(PlayerBuyEvent):
                    Injection.InstallCreateAtomicAction(typeof(BuyPlayerActionManager), typeof(PlayerBuyEventHelper));
                    break;
                case nameof(PlayerClaimPropertyEvent):
                    Injection.InstallCreateAtomicAction(typeof(ClaimPropertyPlayerActionManager), typeof(PlayerClaimPropertyEventHelper));
                    break;
                case nameof(PlayerCompleteContractEvent):
                    Injection.InstallCreateAtomicAction(typeof(CompleteContractPlayerActionManager), typeof(PlayerCompleteContractEventHelper));
                    break;
                case nameof(PlayerCraftEvent):
                    Injection.InstallCreateAtomicAction(typeof(CraftPlayerActionManager), typeof(PlayerCraftEventHelper));
                    break;
                case nameof(PlayerEatEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(Stomach), typeof(PlayerEatEventHelper), "Eat");
                    break;
                case nameof(PlayerGainSkillEvent):
                    Injection.InstallCreateAtomicAction(typeof(GainSkillPlayerActionManager), typeof(PlayerGainSkillEventHelper));
                    break;
                case nameof(PlayerGetElectedEvent):
                    Injection.InstallCreateAtomicAction(typeof(GetElectedPlayerActionManager), typeof(PlayerGetElectedEventHelper));
                    break;
                case nameof(PlayerHarvestEvent):
                    Injection.InstallCreateAtomicAction(typeof(HarvestPlayerActionManager), typeof(PlayerHarvestEventHelper));
                    break;
                case nameof(PlayerInteractEvent):
                    Injection.InstallWithOriginalHelperPublicStatic(typeof(InteractionExtensions), typeof(PlayerInteractEventHelper), "MakeContext");
                    break;
                case nameof(PlayerLoginEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLoginEventHelper), "Login");
                    break;
                case nameof(PlayerLogoutEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLogoutEventHelper), "Logout");
                    break;
                case nameof(PlayerPayTaxEvent):
                    Injection.InstallCreateAtomicAction(typeof(PayTaxPlayerActionManager), typeof(PlayerPayTaxEventHelper));
                    break;
                case nameof(PlayerPickUpEvent):
                    Injection.InstallCreateAtomicAction(typeof(PickUpPlayerActionManager), typeof(PlayerPickUpEventHelper));
                    break;
                case nameof(PlayerPlaceEvent):
                    Injection.InstallCreateAtomicAction(typeof(PlacePlayerActionManager), typeof(PlayerPlaceEventHelper));
                    break;
                case nameof(PlayerProposeVoteEvent):
                    Injection.InstallCreateAtomicAction(typeof(ProposeVotePlayerActionManager), typeof(PlayerProposeVoteEventHelper));
                    break;
                case nameof(PlayerReceiveGovernmentFundsEvent):
                    Injection.InstallCreateAtomicAction(typeof(ReceiveGovernmentFundsPlayerActionManager), typeof(PlayerReceiveGovernmentFundsEventHelper));
                    break;
                case nameof(PlayerRunForElectionEvent):
                    Injection.InstallCreateAtomicAction(typeof(RunForElectionPlayerActionManager), typeof(PlayerRunForElectionEventHelper));
                    break;
                case nameof(PlayerSellEvent):
                    Injection.InstallCreateAtomicAction(typeof(SellPlayerActionManager), typeof(PlayerSellEventHelper));
                    break;
                case nameof(PlayerSendMessageEvent):
                    Injection.InstallCreateAtomicAction(typeof(MessagePlayerActionManager), typeof(PlayerSendMessageEventHelper));
                    break;
                case nameof(PlayerTeleportEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(Player), typeof(PlayerTeleportEventHelper), "SetPosition");
                    break;
                case nameof(PlayerUnlearnSkillEvent):
                    Injection.InstallCreateAtomicAction(typeof(UnlearnSkillPlayerActionManager), typeof(PlayerUnlearnSkillEventHelper));
                    break;
                case nameof(PlayerVoteEvent):
                    Injection.InstallCreateAtomicAction(typeof(VotePlayerActionManager), typeof(PlayerVoteEventHelper));
                    break;

                // RPC Events
                case nameof(RpcInvokeEvent):
                    Injection.Install(typeof(RPCManager).GetMethods(Injection.PUBLIC_STATC).First(mi => mi.Name == "InvokeOn" && mi.GetParameters().Length == 5), typeof(RpcInvokeEventHelper));
                    break;

                // World Events

                case nameof(WorldPolluteEvent):
                    Injection.InstallCreateAtomicAction(typeof(PolluteAirPlayerActionManager), typeof(WorldPolluteEventHelper));
                    break;

                // WorldObject Events

                case nameof(RubbleSpawnEvent):
                    Injection.InstallWithOriginalHelperPublicStatic(typeof(EcoObjectManager), typeof(RubbleSpawnEventHelper), "Add");
                    break;
                case nameof(TreeFellEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(TreeEntity), typeof(TreeFellEventHelper), "FellTree");
                    break;
                case nameof(TreeChopEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(TreeEntity), typeof(TreeChopEventHelper), "TryApplyDamage");
                    break;

                case nameof(WorldObjectChangeTextEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(CustomTextComponent), typeof(WorldObjectChangeTextEventHelper), "SetText");
                    break;
                case nameof(WorldObjectDestroyedEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectDestroyedEventHelper), "Destroy");
                    break;
                case nameof(WorldObjectEnabledChangedEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectEnabledChangedEventHelper), "set_Enabled");
                    break;
                case nameof(WorldObjectNameChangedEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectNameChangedEventHelper), "SetName");
                    break;
                case nameof(WorldObjectOperatingChangedEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectOperatingChangedEventHelper), "set_Operating");
                    break;
                case nameof(WorldObjectPickupEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectPickupEventHelper), "TryPickUp");
                    break;
            }
        }
    }
}
