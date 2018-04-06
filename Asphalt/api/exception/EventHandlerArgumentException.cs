/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 30, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.api.exception
{
    public class EventHandlerArgumentException : Exception
    {
        public EventHandlerArgumentException()
        { }

        public EventHandlerArgumentException(string message)
            : base(message)
        { }

        public EventHandlerArgumentException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
