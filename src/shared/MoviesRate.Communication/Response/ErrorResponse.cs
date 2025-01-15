namespace MoviesRate.Communication.Response;

public class ErrorResponse
{
    public ErrorResponse(IList<string> errors) => Errors = errors;

    public ErrorResponse(string error) => Errors = [error];

    public IList<string> Errors { get; set; }
}