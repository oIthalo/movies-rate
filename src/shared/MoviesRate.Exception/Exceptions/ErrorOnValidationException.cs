using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class ErrorOnValidationException : MoviesRateException
{
    private readonly IList<string> _errors;

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override IList<string> GetErrorMessages() => _errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}