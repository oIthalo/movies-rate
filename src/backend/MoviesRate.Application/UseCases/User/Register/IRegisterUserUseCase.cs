using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ShortUserResponse> Execute(RegisterUserRequest request);
}