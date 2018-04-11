using Asphalt.Api.Event;
using System.Collections.Generic;
using System.Reflection;

namespace Asphalt.Events
{
    internal struct EventHandlerData
    {
        public MethodInfo Method { get; }

        public EventPriority Priority { get; }

        public object Listener { get; }

        public bool RunIfEventCancelled { get; }

        internal EventHandlerData(object listener, MethodInfo method, EventPriority priority, bool runIfEventCancelled = false)
        {
            Listener = listener;
            Method = method;
            Priority = priority;
            RunIfEventCancelled = runIfEventCancelled;
        }
    }

    internal class EventHandlerComparer : IComparer<EventHandlerData>
    {
        public int Compare(EventHandlerData x, EventHandlerData y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
