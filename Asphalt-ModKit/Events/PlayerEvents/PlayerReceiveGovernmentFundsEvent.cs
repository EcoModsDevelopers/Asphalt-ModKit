using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerReceiveGovernmentFundsEvent : CancellableEvent
    {
        public User User { get; protected set; }

        public Currency Currency { get; set; }

        public float Amount { get; set; }

        public PlayerReceiveGovernmentFundsEvent(User pUser, Currency pCurrency, float pAmount) : base()
        {
            this.User = pUser;
            this.Currency = pCurrency;
            this.Amount = pAmount;
        }
    }

    internal class PlayerReceiveGovernmentFundsEventHelper
    {
        public IAtomicAction CreateAtomicAction(User user, Currency currency, float amount)
        {
            PlayerReceiveGovernmentFundsEvent cEvent = new PlayerReceiveGovernmentFundsEvent(user, currency, amount);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.User, cEvent.Currency, cEvent.Amount);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(User user, Currency currency, float amount)
        {
            throw new InvalidOperationException();
        }
    }
}
