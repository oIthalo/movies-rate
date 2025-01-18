using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class InvalidPageException : MoviesRateException
{
    public InvalidPageException() : base(MessagesException.INVALID_PAGE)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}