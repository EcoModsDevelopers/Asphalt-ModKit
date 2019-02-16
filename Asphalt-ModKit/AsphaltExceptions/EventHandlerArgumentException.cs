namespace Asphalt.AsphaltExceptions
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
