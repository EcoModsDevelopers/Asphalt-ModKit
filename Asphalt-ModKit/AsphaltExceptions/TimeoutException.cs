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
