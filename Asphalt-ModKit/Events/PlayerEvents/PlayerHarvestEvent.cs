using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Simulation.Agents;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerHarvestEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Organism Target { get; set; }

        public PlayerHarvestEvent(Player pPlayer, Organism pTarget) : base()
        {
            this.Player = pPlayer;
            this.Target = pTarget;
        }
    }

    internal class PlayerHarvestEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, Organism target)
        {
            PlayerHarvestEvent cEvent = new PlayerHarvestEvent(player, target);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.Player, cEvent.Target);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, Organism target)
        {
            throw new InvalidOperationException();
        }
    }
}
