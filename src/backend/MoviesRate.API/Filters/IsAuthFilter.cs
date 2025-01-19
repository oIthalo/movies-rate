using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Security.Tokens.Validator;
using MoviesRate.Exception;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.API.Filters;

public class IsAuthFilter : IAsyncAuthorizationFilter
{
    private readonly ITokenValidator _tokenValidator;
    private readonly IReadUserRepository _userRepository;

    public IsAuthFilter(
        ITokenValidator tokenValidator,
        IReadUserRepository userRepository)
    {
        _tokenValidator = tokenValidator;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _tokenValidator.ValidateAndGetUserIdentifier(token);

            var user = await _userRepository.GetUserByIdentifier(userIdentifier) ?? throw new UnauthorizedException(MessagesException.USER_WITHOUT_PERMISSION);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse("Token is expired.")
            {
                TokenIsExpired = true,
            });
        }
        catch (MoviesRateException moviesRateException)
        {
            context.HttpContext.Response.StatusCode = (int)moviesRateException.GetStatusCode();
            context.Result = new ObjectResult(new ErrorResponse(moviesRateException.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(MessagesException.USER_WITHOUT_PERMISSION));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))
            throw new UnauthorizedException(MessagesException.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}