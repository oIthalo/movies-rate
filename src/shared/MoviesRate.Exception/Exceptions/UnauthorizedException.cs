using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class UnauthorizedException : MoviesRateException
{
    public UnauthorizedException(string message) : base(message) =>
        ErrorMessages = [message];

    public IList<string> ErrorMessages { get; set; }

    public override IList<string> GetErrorMessages() => ErrorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}