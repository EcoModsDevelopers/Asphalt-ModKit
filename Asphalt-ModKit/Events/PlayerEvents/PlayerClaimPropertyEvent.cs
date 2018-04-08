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
        public Player Player { get; protected set; }

        public Vector3i Position { get; protected set; }

        public PlayerClaimPropertyEvent(Player pPlayer, Vector3i pPosition) : base()
        {
            this.Player = pPlayer;
            this.Position = pPosition;
        }
    }

    internal class PlayerClaimPropertyEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, Vector3i position)
        {
            PlayerClaimPropertyEvent cEvent = new PlayerClaimPropertyEvent(player, position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(player, position);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, Vector3i position)
        {
            throw new InvalidOperationException();
        }
    }
}
