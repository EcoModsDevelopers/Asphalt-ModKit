/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Asphalt.api.Event
{
    public class EventService
    {
        ArrayList listeners = new ArrayList();
        DataTable handlers = new DataTable("EventHandlers");

        //Registration

        public void RegisterListener(IListener listener)
        {
            this.listeners.Add(listener);
        }

        public void UnregisterListener(IListener listener)
        {
            this.listeners.Remove(listener);
        }

        public void UnregisterAllListeners()
        {
            this.listeners.Clear();
        }

        //Execution

        public void CallEvent(Event _event) {
            ExecuteListeners(_event.GetName(), _event);
        }

        private void ExecuteListeners(string eventName, Event _event)
        {
            foreach (IListener listener in listeners)
            {
                MethodInfo[] methods = listener.GetType().GetMethods();

                foreach (MethodInfo method in methods)
                {
                    EventHandlerAttribute[] attributes = ((EventHandlerAttribute[])method.GetCustomAttributes(typeof(EventHandlerAttribute), false));

                    if (attributes.FirstOrDefault() == null) return;

                    foreach (EventHandlerAttribute attribute in attributes)
                    {
                        if (attribute.GetName().Equals(_event.GetName()))
                            method.Invoke(listener, new object[] { _event });
                    }
                }
            }
        }
    }
}
