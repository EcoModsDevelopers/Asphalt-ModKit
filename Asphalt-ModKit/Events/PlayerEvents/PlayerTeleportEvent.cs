using Asphalt.Events;
using Eco.Gameplay.Players;
using Eco.Shared.Math;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    ///  Called when a player teleports
    /// </summary>
    public class PlayerTeleportEvent : CancellableEvent, IEvent
    {
        public Player Player { get; protected set; }
        public Vector3 Position { get; protected set; }

        public PlayerTeleportEvent(Player pPlayer, Vector3 pPosition) : base()
        {
            this.Player = pPlayer;
            this.Position = pPosition;
        }
    }

    internal class PlayerTeleportEventHelper
    {
        public void SetPosition(Vector3 position)
        {
            PlayerTeleportEvent pte = new PlayerTeleportEvent((Player)((object)this), position);
            IEvent pteEvent = pte;

            EventManager.CallEvent(ref pteEvent);

            if(!pte.IsCancelled())
                SetPosition_original(position);
        }

        public void SetPosition_original(Vector3 position)
        {
            throw new InvalidOperationException();
        }
    }
}
