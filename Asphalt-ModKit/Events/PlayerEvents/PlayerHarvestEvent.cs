using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Simulation.Agents;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerHarvestEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Organism Target { get; set; }

        public PlayerHarvestEvent(ref Player pPlayer, ref Organism pTarget) : base()
        {
            this.Player = pPlayer;
            this.Target = pTarget;
        }
    }

    internal class PlayerHarvestEventHelper
    {
        public static bool Prefix(ref Player player, ref Organism target, ref IAtomicAction __result)
        {
            PlayerHarvestEvent cEvent = new PlayerHarvestEvent(ref player, ref target);
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
