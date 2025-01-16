using Microsoft.IdentityModel.Tokens;
using MoviesRate.Domain.Security.Tokens.Validator;
using MoviesRate.Infrastructure.Security.Tokens.Access;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoviesRate.Infrastructure.Security.Tokens.Validator;

public class TokenValidator : JwtTokenHandler, ITokenValidator
{
    private readonly string _signinKey;

    public TokenValidator(string signinKey) =>
        _signinKey = signinKey;

    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signinKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        var userIdentifier = principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(userIdentifier);
    }
}