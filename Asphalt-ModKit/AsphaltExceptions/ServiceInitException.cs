using System;

namespace Asphalt.AsphaltExceptions
{
    public class ServiceInitException : Exception
    {
        public ServiceInitException()
        { }

        public ServiceInitException(string message)
            : base(message)
        { }

        public ServiceInitException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
