/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on April 6, 2018
 * ------------------------------------
 **/

using Asphalt.Api.Event.PlayerEvents;
using Eco.Gameplay.Players;
using Eco.Shared.Services;

namespace Asphalt.Api.Event.Player
{
    /**
     * Called when a player sends a chat message;
     * */
    public class PlayerSendMessageEvent : PlayerEvent, ICancellable
    {
        private bool cancel;
        
        public ChatMessage Message { get; protected set; }

        public PlayerSendMessageEvent(User user, ChatMessage message) : base(user)
        {
            this.cancel = false;
            this.Message = message;
        }

        public bool IsCancelled()
        {
            return this.cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }
}
