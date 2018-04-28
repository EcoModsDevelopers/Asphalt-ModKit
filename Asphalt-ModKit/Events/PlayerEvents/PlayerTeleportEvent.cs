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
        public static bool Prefix(Vector3 position, Player __instance)
        {
            PlayerTeleportEvent cEvent = new PlayerTeleportEvent(__instance, position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            return cEvent.IsCancelled();
        }
    }
}
