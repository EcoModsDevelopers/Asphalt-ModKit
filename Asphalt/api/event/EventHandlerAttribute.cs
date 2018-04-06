/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.api.Event
{
    [AttributeUsage(AttributeTargets.Method)]
    class EventHandlerAttribute : Attribute
    {
        public EventPriority priority { get; set; }

        public EventHandlerAttribute()
        {
            this.priority = EventPriority.NORMAL;
        }

        public EventHandlerAttribute(EventPriority priority)
        {
            this.priority = priority;
        }

        public EventPriority GetPriority()
        {
            return this.priority;
        }
    }
}
