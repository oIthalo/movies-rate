using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class NotFoundException : MoviesRateException
{
    public NotFoundException(string error) : base(string.Empty)
    {
        Errors = [error];
    }

    public IList<string> Errors { get; set; }

    public override IList<string> GetErrorMessages() => Errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}