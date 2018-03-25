/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Asphalt.api.Event
{
    public class EventService
    {
        private bool populated;

        List<IListener> listeners = new List<IListener>();
        Dictionary<EventPriority, List<EventHandler>> handlers = new Dictionary<EventPriority, List<EventHandler>>();

        struct EventHandler
        {
            public IListener listener;
            public MethodInfo method;
            public string eventName;
        }

        //Registration

        public void RegisterListener(IListener listener)
        {
            this.listeners.Add(listener);
            this.populated = false;
        }

        //Execution

        public void CallEvent(Event _event) {
            ExecuteListeners(_event.GetName(), _event);
        }

        private void ExecuteListeners(string eventName, Event _event)
        {
            if (!populated) populateHandlerList();

            foreach (var list in handlers)
            {
                foreach (EventHandler handler in list.Value)
                {
                    if (handler.eventName.Equals(_event.GetName()))
                        handler.method.Invoke(handler.listener, new object[] { _event });
                }
            }
        }

        //Initialization

        public void populateHandlerList()
        {
            handlers.Clear();

            handlers.Add(EventPriority.HIGHEST, new List<EventHandler>());
            handlers.Add(EventPriority.HIGH, new List<EventHandler>());
            handlers.Add(EventPriority.NORMAL, new List<EventHandler>());
            handlers.Add(EventPriority.LOW, new List<EventHandler>());
            handlers.Add(EventPriority.LOWEST, new List<EventHandler>()); 

            foreach (IListener listener in listeners)
            {
                MethodInfo[] methods = listener.GetType().GetMethods();

                foreach (MethodInfo method in methods)
                {
                    EventHandlerAttribute[] attributes = ((EventHandlerAttribute[])method.GetCustomAttributes(typeof(EventHandlerAttribute), false));

                    if (attributes.FirstOrDefault() == null) continue;

                    foreach (EventHandlerAttribute attribute in attributes)
                    {
                        EventHandler handler;
                        handler.listener = listener;
                        handler.method = method;
                        handler.eventName = attribute.GetName();

                        handlers[attribute.GetPriority()].Add(handler);
                    }
                }
            }

            this.populated = true;
        }
    }
}
