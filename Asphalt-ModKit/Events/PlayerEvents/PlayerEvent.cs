/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Eco.Gameplay.Players;

/**
 * Events triggered by a player.
 **/
namespace Asphalt.Api.Event.PlayerEvents
{
    public abstract class PlayerEvent : Event
    {
        public User User { get; protected set; }

        public PlayerEvent(User user) : base()
        {
            this.User = user;
        }
    }
}
