using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.Application.UseCases.User.Login;

public interface ILoginUserUseCase
{
    Task<ShortUserResponse> Execute(LoginUserRequest request);
}