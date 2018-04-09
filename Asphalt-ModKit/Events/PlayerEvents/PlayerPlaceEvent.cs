using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Services;
using Eco.Simulation.Agents;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerPlaceEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public BlockItem Item { get; set; }

        public Vector3i Position { get; set; }

        public PlayerPlaceEvent(Player pPlayer, BlockItem pPlacedItem, Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.Item = pPlacedItem;
            this.Position = pPosition;
        }
    }

    internal class PlayerPlaceEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, BlockItem placedItem, Vector3i position)
        {
            PlayerPlaceEvent cEvent = new PlayerPlaceEvent(player, placedItem, position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.Player, cEvent.Item, cEvent.Position);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, BlockItem placedItem, Vector3i position)
        {
            throw new InvalidOperationException();
        }
    }
}
