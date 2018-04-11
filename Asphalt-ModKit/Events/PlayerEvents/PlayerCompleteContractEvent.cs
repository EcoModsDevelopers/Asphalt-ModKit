using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerCompleteContractEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public PlayerCompleteContractEvent(Player pPlayer) : base()
        {
            this.Player = pPlayer;
        }
    }

    internal class PlayerCompleteContractEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player)
        {
            PlayerCompleteContractEvent cEvent = new PlayerCompleteContractEvent(player);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.Player);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player)
        {
            throw new InvalidOperationException();
        }
    }
}
