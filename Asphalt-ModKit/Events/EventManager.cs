/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Asphalt.Api.AsphaltExceptions;
using Asphalt.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Api.Event
{
    public static class EventManager
    {


        //  List<IListener> listeners = new List<IListener>();
        //  Dictionary<EventPriority, List<EventHandler>> handlers = new Dictionary<EventPriority, List<EventHandler>>();

        private static readonly EventHandlerComparer eventHandlerComparer = new EventHandlerComparer();

        //<type of parameter from registered event, List<registered event>>
        private static readonly Dictionary<Type, List<EventHandlerData>> handlers = new Dictionary<Type, List<EventHandlerData>>();

        //Registration

        public static void RegisterListener(IListener pListener)
        {
            MethodInfo[] methods = pListener.GetType().GetMethods(BindingFlags.Public); //static ?!?

            foreach (MethodInfo method in methods)
            {
                EventHandlerAttribute attribute = method.GetCustomAttributes<EventHandlerAttribute>(false)?.FirstOrDefault();

                if (attribute == null)
                    continue;

                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length != 1)
                    throw new EventHandlerArgumentException("Incorrect number of arguments in method with EventHandlerAttribute!");

                Type parameterType = parameters[0].ParameterType;

                if (!parameterType.IsSubclassOf(typeof(Event)))
                    throw new EventHandlerArgumentException("Specified argument is not a valid Event!");

                if (!handlers.ContainsKey(parameterType))
                    handlers.Add(parameterType, new List<EventHandlerData>());

                handlers[parameterType].Add(new EventHandlerData(pListener, method, attribute.Priority, attribute.RunIfEventCancelled));
                handlers[parameterType].Sort(eventHandlerComparer);
            }
        }

        public static void CallEvent(ref Event pEvent)
        {
            bool cancelable = pEvent.GetType().GetInterfaces().Contains(typeof(ICancellable));

            foreach (var eventHandlerData in handlers[pEvent.GetType()])
            {
                try
                {
                    if (cancelable && ((ICancellable)pEvent).IsCancelled() && !eventHandlerData.RunIfEventCancelled)
                        continue;

                    //Invoke EventHandler
                    eventHandlerData.Method.Invoke(eventHandlerData.Listener, new object[] { pEvent });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            /*
                 //Cancel following EventHandlers if event IsCancelled
                if (!_event.GetType().GetInterfaces().Contains(typeof(ICancellable)))
                    continue;
                if (((ICancellable)_event).IsCancelled())
                    return;

             */
        }

    }
}
