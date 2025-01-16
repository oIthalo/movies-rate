using MoviesRate.Domain.Security.Tokens.Access;
using MoviesRate.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;

public class AccessTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(1000, "jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj");
}