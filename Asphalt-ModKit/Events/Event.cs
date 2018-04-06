/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

namespace Asphalt.Api.Event
{
    public abstract class Event
    {
        private readonly string Name;

        public Event()
        {
            this.Name = this.GetType().Name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
