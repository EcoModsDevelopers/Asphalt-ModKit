/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 30, 2018
 * ------------------------------------
 **/

using System;

namespace Asphalt.AsphaltExceptions
{
    public class TimeoutException : Exception
    {
        public TimeoutException()
        { }

        public TimeoutException(string message)
            : base(message)
        { }

        public TimeoutException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
