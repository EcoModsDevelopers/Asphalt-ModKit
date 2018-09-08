using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerSellEvent : CancellableEvent
    {
        public User User { get; set; }

        public StoreComponent Store { get; set; }

        public Item Item { get; set; }

        public PlayerSellEvent(ref User pUser, ref StoreComponent pStore, ref Item pItem) : base()
        {
            this.User = pUser;
            this.Store = pStore;
            this.Item = pItem;
        }
    }

    internal class PlayerSellEventHelper
    {
        public static bool Prefix(ref User user, ref StoreComponent store, ref Item item, ref IAtomicAction __result)
        {
            PlayerSellEvent cEvent = new PlayerSellEvent(ref user, ref store, ref item);
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
