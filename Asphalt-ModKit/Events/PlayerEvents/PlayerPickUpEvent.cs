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

        public Item PickedUpItem { get; set; }

        public Type ItemType { get; set; }

        public Vector3i Position { get; set; }

        public PlayerPickUpEvent(ref Player pPlayer, ref Item pPickedUpItem, ref Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.PickedUpItem = pPickedUpItem;
            this.ItemType = pPickedUpItem.GetType();
            this.Position = pPosition;
        }

        public PlayerPickUpEvent(ref Player pPlayer, ref Type pItemType, ref Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.PickedUpItem = null;
            this.ItemType = pItemType;
            this.Position = pPosition;
        }
    }

    internal class PlayerPickUpEventHelper1
    {
        public static bool Prefix(ref Player actor, ref Item pickedUpItem, ref Vector3i position, ref IAtomicAction __result)
        {
            PlayerPickUpEvent cEvent = new PlayerPickUpEvent(ref actor, ref pickedUpItem, ref position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }

    internal class PlayerPickUpEventHelper2
    {
        public static bool Prefix(ref Player actor, ref Type itemType, ref Vector3i position, ref IAtomicAction __result)
        {
            PlayerPickUpEvent cEvent = new PlayerPickUpEvent(ref actor, ref itemType, ref position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
