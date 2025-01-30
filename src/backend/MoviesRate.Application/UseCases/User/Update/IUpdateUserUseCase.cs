using MoviesRate.Communication.Requests;

namespace MoviesRate.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(UpdateUserRequest request);
}