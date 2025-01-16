namespace MoviesRate.Domain.Security.Tokens.Validator;

public interface ITokenValidator
{
    Guid ValidateAndGetUserIdentifier(string token);
}