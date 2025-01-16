namespace MoviesRate.Domain.Security.Tokens.Access;

public interface IAccessTokenGenerator
{
    string Generate(Guid userIdentifier);
}