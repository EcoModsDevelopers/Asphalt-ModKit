/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Eco.Gameplay.Interactions;

namespace Asphalt.Api.Event.PlayerEvents
{
    /**
     * Called when a player interacts with an object.
     * */
     /*
    public class PlayerInteractObjectEvent : PlayerEvent, ICancellable
    {
        private bool Cancel;
        private readonly InteractionContext Context;

        public PlayerInteractObjectEvent(InteractionContext context) : base(context.Player)
        {
            this.Cancel = false;
            this.Context = context;
        }

        public bool IsCancelled()
        {
            return this.Cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.Cancel = cancel;
        }

        public InteractionContext GetContext()
        {
            return this.Context;
        }
    }*/
}
