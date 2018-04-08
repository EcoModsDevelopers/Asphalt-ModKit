/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 30, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.Api.AsphaltExceptions
{
    public class EventHandlerArgumentException : System.Exception
    {
        public EventHandlerArgumentException()
        { }

        public EventHandlerArgumentException(string message)
            : base(message)
        { }

        public EventHandlerArgumentException(string message, System.Exception inner)
            : base(message, inner)
        { }
    }
}
