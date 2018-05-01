using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player claims new property
    /// </summary>
    public class PlayerClaimPropertyEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Vector3i Position { get; set; }

        public PlayerClaimPropertyEvent(Player pPlayer, Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.Position = pPosition;
        }
    }

    internal class PlayerClaimPropertyEventHelper
    {
        public static bool Prefix(Player player, Vector3i position, ref IAtomicAction __result)
        {
            PlayerClaimPropertyEvent cEvent = new PlayerClaimPropertyEvent(player, position);
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
