using System.Net;

namespace MoviesRate.Exception.Exceptions;

public abstract class MoviesRateException : SystemException
{
    protected MoviesRateException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}