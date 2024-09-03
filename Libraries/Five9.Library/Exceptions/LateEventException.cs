namespace Five9.Library.Exceptions
{
    public class LateEventException : Exception
    {
        public LateEventException() { }

        public LateEventException(string message)
        : base(message)
        {
        }

        public LateEventException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
