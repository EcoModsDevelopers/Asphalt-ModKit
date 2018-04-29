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
    public class ServiceReloadException : Exception
    {
        public ServiceReloadException()
        { }

        public ServiceReloadException(string message)
            : base(message)
        { }

        public ServiceReloadException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
