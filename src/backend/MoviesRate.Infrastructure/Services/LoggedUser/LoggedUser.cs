using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Security.Tokens.Provider;
using MoviesRate.Domain.Services.LoggedUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoviesRate.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly IReadUserRepository _readUserRepository;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(
        IReadUserRepository readUserRepository, 
        ITokenProvider tokenProvider)
    {
        _readUserRepository = readUserRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
        var userIdentifier = Guid.Parse(identifier);

        return await _readUserRepository.GetUserByIdentifier(userIdentifier);
    }
}