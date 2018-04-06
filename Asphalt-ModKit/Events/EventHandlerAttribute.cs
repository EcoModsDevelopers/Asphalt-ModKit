/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.Api.Event
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventHandlerAttribute : Attribute
    {
        public EventPriority Priority { get; set; } = EventPriority.Normal;

        public bool RunIfEventCancelled { get; set; } = false;

        public EventHandlerAttribute() { }

        public EventHandlerAttribute(EventPriority priority)
        {
            this.Priority = priority;
        }
    }
}
