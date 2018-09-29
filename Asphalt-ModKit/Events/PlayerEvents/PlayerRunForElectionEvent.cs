﻿using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerRunForElectionEvent : CancellableEvent
    {
        public User User { get; set; }

        public PlayerRunForElectionEvent(ref User pUser) : base()
        {
            this.User = pUser;
        }
    }

    internal class PlayerRunForElectionEventHelper
    {
        public static bool Prefix(ref User user, ref IAtomicAction __result)
        {
            PlayerRunForElectionEvent cEvent = new PlayerRunForElectionEvent(ref user);
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
