using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MoviesRate.Application.UseCases.User.Register;
using MoviesRate.Communication.Response;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;

namespace UseCase.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().BeOfType<ShortUserResponse>();
        result.Should().NotBeNull();
        result.Name.Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        (var user, _) = UserBuilder.Build();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);
        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(x => x.Errors.Contains(MessagesException.EMAIL_ALREADY_REGISTERED));
    }

    private static RegisterUserUseCase CreateUseCase(MoviesRate.Domain.Entities.User? user = default!)
    {
        var readUserRepository = new ReadUserRepositoryBuilder();
        var writeUserRepository = WriteUserRepositoryBuilder.Build();
        var autoMapper = AutoMapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();

        if (user is not null)
            readUserRepository.ExistActiveUserWithEmail(user);

        return new RegisterUserUseCase(readUserRepository.Build(), writeUserRepository, autoMapper, unitOfWork, passwordEncripter);
    }
}