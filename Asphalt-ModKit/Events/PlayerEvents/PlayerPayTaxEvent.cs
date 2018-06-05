using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerPayTaxEvent : CancellableEvent
    {
        public User User { get; set; }

        public Currency Currency { get; set; }

        public float Amount { get; set; }

        public PlayerPayTaxEvent(ref User pUser, ref Currency pCurrency, ref float pAmount) : base()
        {
            this.User = pUser;
            this.Currency = pCurrency;
            this.Amount = pAmount;
        }
    }

    internal class PlayerPayTaxEventHelper
    {
        public static bool Prefix(ref User user, ref Currency currency, ref float amount, ref IAtomicAction __result)
        {
            PlayerPayTaxEvent cEvent = new PlayerPayTaxEvent(ref user, ref currency, ref amount);
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
