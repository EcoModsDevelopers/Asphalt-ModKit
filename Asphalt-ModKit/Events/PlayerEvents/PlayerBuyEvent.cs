using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerBuyEvent : CancellableEvent
    {
        public User User { get; set; }

        public StoreComponent Store { get; set; }

        public Item Item { get; set; }

        public int Count { get; set; }

        public PlayerBuyEvent(ref User pUser, ref StoreComponent pStore, ref Item pItem, ref int pCount) : base()
        {
            this.User = pUser;
            this.Store = pStore;
            this.Item = pItem;
            this.Count = pCount;
        }
    }

    internal class PlayerBuyEventHelper
    {
        public static bool Prefix(ref User actor, ref StoreComponent store, ref Item item, ref int count, ref IAtomicAction __result)
        {
            PlayerBuyEvent cEvent = new PlayerBuyEvent(ref actor, ref store, ref item, ref count);
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
