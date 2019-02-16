using System;

namespace Asphalt.AsphaltExceptions
{
    public class ServiceAlreadyExistingException : Exception
    {
        public ServiceAlreadyExistingException()
        { }

        public ServiceAlreadyExistingException(string message)
            : base(message)
        { }

        public ServiceAlreadyExistingException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
