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
        public string name { get; set; }
        public EventPriority priority { get; set; }

        public EventHandlerAttribute(string name)
        {
            this.name = name;
            this.priority = EventPriority.NORMAL;
        }

        public EventHandlerAttribute(string name, EventPriority priority)
        {
            this.name = name;
            this.priority = priority;
        }

        public string GetName()
        {
            return this.name;
        }

        public EventPriority GetPriority()
        {
            return this.priority;
        }
    }
}
