﻿using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Services;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player sends a chat message
    /// </summary>
    public class PlayerSendMessageEvent : CancellableEvent
    {
        public User User { get; set; }

        public ChatMessage Message { get; set; }

        public PlayerSendMessageEvent(ref User user, ref ChatMessage message) : base()
        {
            this.User = user;
            this.Message = message;
        }
    }

    internal class PlayerSendMessageEventHelper
    {
        public static bool Prefix(ref User user, ref ChatMessage message, ref IAtomicAction __result)
        {
            PlayerSendMessageEvent cEvent = new PlayerSendMessageEvent(ref user, ref message);
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
