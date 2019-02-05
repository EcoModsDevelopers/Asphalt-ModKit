using System;

namespace Asphalt.Api.Event
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
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
