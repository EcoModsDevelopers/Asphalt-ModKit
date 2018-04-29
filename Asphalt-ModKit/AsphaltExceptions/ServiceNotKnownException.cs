/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 27, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.AsphaltExceptions
{
    public class ServiceNotKnownException : Exception
    {
        public ServiceNotKnownException()
        { }

        public ServiceNotKnownException(string message)
            : base(message)
        { }

        public ServiceNotKnownException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
