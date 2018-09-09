using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerCompleteContractEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public PlayerCompleteContractEvent(ref Player pPlayer) : base()
        {
            this.Player = pPlayer;
        }
    }

    internal class PlayerCompleteContractEventHelper
    {
        public static bool Prefix(ref Player player, ref IAtomicAction __result)
        {
            PlayerCompleteContractEvent cEvent = new PlayerCompleteContractEvent(ref player);
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
