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
namespace Asphalt.api.Event.player
{
    public abstract class PlayerEvent : Event
    {
        private readonly Player player;

        public PlayerEvent(Player player) : base()
        {
            this.player = player;
        }

        public Player GetPlayer()
        {
            return this.player;
        }
    }
}
