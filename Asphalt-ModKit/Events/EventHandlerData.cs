using Asphalt.Api.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
