/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Eco.Gameplay.Interactions;

namespace Asphalt.api.Event.player
{
    public class PlayerInteractObjectEvent : PlayerEvent
    {
        private InteractionContext context;

        public PlayerInteractObjectEvent(InteractionContext context) : base(context.Player)
        {
            this.context = context;
        }

        public InteractionContext GetContext()
        {
            return this.context;
        }
    }
}
