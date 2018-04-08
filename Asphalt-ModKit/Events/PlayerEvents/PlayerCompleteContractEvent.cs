using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerCompleteContractEvent : CancellableEvent
    {
        public Player Player { get; protected set; }

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
                return CreateAtomicAction_original(player);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player)
        {
            throw new InvalidOperationException();
        }
    }
}
