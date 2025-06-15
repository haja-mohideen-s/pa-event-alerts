public class PAEventException : Exception
{
    public PAEventException()
    {
    }

    public PAEventException(string message)
        : base(message)
    {
    }

    public PAEventException(string message, Exception inner)
        : base(message, inner)
    {
    }
}