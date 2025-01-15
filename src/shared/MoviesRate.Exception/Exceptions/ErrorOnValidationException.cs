using System.Net;

namespace MoviesRate.Exception.Exceptions;

public class ErrorOnValidationException : MoviesRateException
{
    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }

    public IList<string> Errors {  get; set; }

    public override IList<string> GetErrorMessages() => Errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}