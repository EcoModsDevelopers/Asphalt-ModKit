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
        public User User { get; set; }

        public Vector2i Position { get; set; }

        public Guid AuthId { get; set; }

        public PlayerClaimPropertyEvent(ref Guid pAuthId, ref User pUser, ref Vector2i pPosition) : base()
        {
            this.AuthId = pAuthId;
            this.User = pUser;
            this.Position = pPosition;
        }
    }

    internal class PlayerClaimPropertyEventHelper
    {
        public static bool Prefix(ref Guid authId, ref User actor, ref Vector2i position, ref IAtomicAction __result)
        {
            PlayerClaimPropertyEvent cEvent = new PlayerClaimPropertyEvent(ref authId, ref actor, ref position);
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
