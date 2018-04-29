using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerBuyEvent : CancellableEvent
    {
        public User User { get; set; }

        public StoreComponent Store { get; set; }

        public Item Item { get; set; }

        public PlayerBuyEvent(User pUser, StoreComponent pStore, Item pItem) : base()
        {
            this.User = pUser;
            this.Store = pStore;
            this.Item = pItem;
        }
    }

    internal class PlayerBuyEventHelper
    {
        public static bool Prefix(User user, StoreComponent store, Item item, ref IAtomicAction __result)
        {
            PlayerBuyEvent cEvent = new PlayerBuyEvent(user, store, item);
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
