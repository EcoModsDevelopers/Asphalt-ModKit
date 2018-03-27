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
    /**
     * Called when a player interacts with an object.
     * */
    public class PlayerInteractObjectEvent : PlayerEvent, ICancellable
    {
        private bool cancel;
        private readonly InteractionContext context;

        public PlayerInteractObjectEvent(InteractionContext context) : base(context.Player)
        {
            this.cancel = false;
            this.context = context;
        }

        public bool IsCancelled()
        {
            return this.cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.cancel = cancel;
        }

        public InteractionContext GetContext()
        {
            return this.context;
        }
    }
}
