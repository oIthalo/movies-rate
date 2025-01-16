using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class InvalidLoginException : MoviesRateException
{
    public InvalidLoginException() : base(MessagesException.INVALID_LOGIN)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}