using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerGetElectedEvent : CancellableEvent
    {
        public User User { get; set; }

        public PlayerGetElectedEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerGetElectedEventHelper
    {
        public static bool Prefix(ref User actor, ref IAtomicAction __result)
        {
            PlayerGetElectedEvent cEvent = new PlayerGetElectedEvent(ref actor);
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
