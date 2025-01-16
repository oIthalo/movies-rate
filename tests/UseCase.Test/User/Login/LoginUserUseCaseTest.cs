using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using MoviesRate.Application.UseCases.User.Login;
using MoviesRate.Communication.Response;
using MoviesRate.Exception.Exceptions;
using MoviesRate.Exception;
using MoviesRate.Communication.Requests;
using MoviesRate.Domain.Entities;

namespace UseCase.Test.User.Login;

public class LoginUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new LoginUserRequest()
        {
            Email = user.Email,
            Password = password
        });

        result.Should().BeOfType<ShortUserResponse>();
        result.Should().NotBeNull();
        result.Name.Should().NotBeNullOrWhiteSpace();
        result.Tokens.Should().NotBeNull();
    }

    [Fact]
    public async Task Error_Invalid_Login()
    {
        (var user, var password) = UserBuilder.Build();
        var request = LoginUserRequestBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<InvalidLoginException>()
            .Where(x => x.Message.Equals(MessagesException.INVALID_LOGIN));
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var request = LoginUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(x => x.Errors.Contains(MessagesException.USER_NOT_FOUND));
    }

    private static LoginUserUseCase CreateUseCase(MoviesRate.Domain.Entities.User? user = default!)
    {
        var readUserRepository = new ReadUserRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();

        if (user is not null)
            readUserRepository.GetUserByEmail(user);

        return new LoginUserUseCase(readUserRepository.Build(), passwordEncripter, accessTokenGenerator);
    }
}