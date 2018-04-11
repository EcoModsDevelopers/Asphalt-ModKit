using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerPickUpEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public BlockItem PickedUpItem { get; set; }

        public Vector3i Position { get; set; }

        public PlayerPickUpEvent(Player pPlayer, BlockItem pPickedUpItem, Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.PickedUpItem = pPickedUpItem;
            this.Position = pPosition;
        }
    }

    internal class PlayerPickUpEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, BlockItem pickedUpItem, Vector3i position)
        {
            PlayerPickUpEvent cEvent = new PlayerPickUpEvent(player, pickedUpItem, position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.Player, cEvent.PickedUpItem, cEvent.Position);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, BlockItem pickedUpItem, Vector3i position)
        {
            throw new InvalidOperationException();
        }
    }
}
