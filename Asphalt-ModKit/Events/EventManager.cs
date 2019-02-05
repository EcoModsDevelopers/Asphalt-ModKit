/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Asphalt.AsphaltExceptions;
using Asphalt.Events;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Api.Event
{
    public static class EventManager
    {

        private static readonly EventHandlerComparer eventHandlerComparer = new EventHandlerComparer();

        //<type of parameter from registered event, List<registered event>>
        private static readonly Dictionary<Type, List<EventHandlerData>> handlers = new Dictionary<Type, List<EventHandlerData>>();

        private static object locker = new object();


        //Registration

        public static void RegisterListener(object pListener)
        {
            MethodInfo[] methods = pListener.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance); //static ?!?

            foreach (MethodInfo method in methods)
            {
                EventHandlerAttribute attribute = method.GetCustomAttributes<EventHandlerAttribute>(false)?.FirstOrDefault();

                if (attribute == null)
                    continue;

                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length != 1)
                    throw new EventHandlerArgumentException("Incorrect number of arguments in method with EventHandlerAttribute!");

                Type parameterType = parameters[0].ParameterType;

                if (!typeof(IEvent).IsAssignableFrom(parameterType))
                    throw new EventHandlerArgumentException("Specified argument is not a valid Event!");

                lock (locker)
                {
                    if (!handlers.ContainsKey(parameterType))
                    {
                        try
                        {
                            EventManagerHelper.injectEvent(parameterType);
                        }
                        catch (Exception e)
                        {
                            Log.WriteError(new LocString(e.ToStringPretty()));
#if DEBUG
                            throw;
#endif
                        }

                        handlers.Add(parameterType, new List<EventHandlerData>());
                    }

                    handlers[parameterType].Add(new EventHandlerData(pListener, method, attribute.Priority, attribute.RunIfEventCancelled));
                    handlers[parameterType].Sort(eventHandlerComparer);
                }
            }
        }

        public static void UnregisterListener(object pListener)
        {
            foreach (KeyValuePair<Type, List<EventHandlerData>> entry in handlers)
                entry.Value.RemoveAll(x => x.Listener.Equals(pListener));
        }

        //Execution

        public static void CallEvent(ref IEvent pEvent)
        {
            //        Console.WriteLine(pEvent);

            if (!handlers.ContainsKey(pEvent.GetType()))
                return;

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
#if DEBUG
                    throw;
#endif
                }
            }
        }

    }
}
