/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on April 6, 2018
 * ------------------------------------
 **/

using Eco.Gameplay.Players;

namespace Asphalt.api.Event.player
{
    /**
     * Called when a player sends a chat message;
     * */
    public class PlayerSendMessageEvent : PlayerEvent, ICancellable
    {
        private bool cancel;

        private int id;
        private int timeSeconds;
        private string channel;
        private string message;


        public PlayerSendMessageEvent(Player player, int id, int timeSeconds, string channel, string message) : base(player)
        {
            this.cancel = false;
            this.id = id;
            this.timeSeconds = timeSeconds;
            this.channel = channel;
            this.message = message;
        }

        public bool IsCancelled()
        {
            return this.cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        public int GetId()
        {
            return this.id;
        }

        public int GetTimeSeconds()
        {
            return this.timeSeconds;
        }

        public string GetChannel()
        {
            return this.channel;
        }

        public string GetMessage()
        {
            return this.message;
        }
    }
}
