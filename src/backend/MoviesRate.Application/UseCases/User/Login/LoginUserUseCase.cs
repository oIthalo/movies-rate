using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Security.Criptography;
using MoviesRate.Domain.Security.Tokens.Access;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.Application.UseCases.User.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IReadUserRepository _readUserRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginUserUseCase(
        IReadUserRepository readUserRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _readUserRepository = readUserRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ShortUserResponse> Execute(LoginUserRequest request)
    {
        Validate(request);

        var user = await _readUserRepository.GetUserByEmail(request.Email) ?? throw new NotFoundException(MessagesException.USER_NOT_FOUND);

        if (user is null || !_passwordEncripter.IsValid(request.Password, user.Password)) throw new InvalidLoginException();

        var token = _accessTokenGenerator.Generate(user.Identifier);

        return new ShortUserResponse()
        {
            Name = user.Name,
            Tokens =
            {
                AccessToken = token,
            }
        };
    }

    private static void Validate(LoginUserRequest request)
    {
        var result = new LoginUserValidator().Validate(request);

        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}