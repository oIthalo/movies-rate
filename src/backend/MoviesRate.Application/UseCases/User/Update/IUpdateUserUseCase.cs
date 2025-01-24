using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task<UpdateUserResponse> Execute(UpdateUserRequest request);
}