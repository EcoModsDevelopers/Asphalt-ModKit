using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerSellEvent : CancellableEvent
    {
        public User User { get; protected set; }

        public StoreComponent Store { get; protected set; }

        public Item Item { get; protected set; }

        public PlayerSellEvent(User pUser, StoreComponent pStore, Item pItem) : base()
        {
            this.User = pUser;
            this.Store = pStore;
            this.Item = pItem;
        }
    }

    internal class PlayerSellEventHelper
    {
        public IAtomicAction CreateAtomicAction(User user, StoreComponent store, Item item)
        {
            PlayerSellEvent cEvent = new PlayerSellEvent(user, store, item);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(user, store, item);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(User user, StoreComponent store, Item item)
        {
            throw new InvalidOperationException();
        }
    }
}
