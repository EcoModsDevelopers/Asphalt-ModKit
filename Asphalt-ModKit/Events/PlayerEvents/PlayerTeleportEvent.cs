using Asphalt.Events;
using Eco.Gameplay.Players;
using Eco.Shared.Math;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    ///  Called when a player teleports
    /// </summary>
    public class PlayerTeleportEvent : CancellableEvent
    {
        public Player Player { get; set; }
        public Vector3 Position { get; set; }

        public PlayerTeleportEvent(ref Player pPlayer, ref Vector3 pPosition) : base()
        {
            this.Player = pPlayer;
            this.Position = pPosition;
        }
    }

    internal class PlayerTeleportEventHelper
    {
        public static bool Prefix(ref Vector3 position, ref Player __instance)
        {
            PlayerTeleportEvent cEvent = new PlayerTeleportEvent(ref __instance, ref position);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            return !cEvent.IsCancelled();
        }
    }
}
