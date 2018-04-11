using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerGetElectedEvent : CancellableEvent
    {
        public User User { get; set; }

        public PlayerGetElectedEvent(User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerGetElectedEventHelper
    {
        public IAtomicAction CreateAtomicAction(User user)
        {
            PlayerGetElectedEvent cEvent = new PlayerGetElectedEvent(user);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(cEvent.User);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(User user)
        {
            throw new InvalidOperationException();
        }
    }
}
