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
