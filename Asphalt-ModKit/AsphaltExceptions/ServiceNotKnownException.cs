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
