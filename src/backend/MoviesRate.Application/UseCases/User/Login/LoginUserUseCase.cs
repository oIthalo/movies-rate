using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Security.Criptography;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.Application.UseCases.User.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IReadUserRepository _readUserRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IMapper _mapper;

    public LoginUserUseCase(
        IReadUserRepository readUserRepository,
        IPasswordEncripter passwordEncripter,
        IMapper mapper)
    {
        _readUserRepository = readUserRepository;
        _passwordEncripter = passwordEncripter;
        _mapper = mapper;
    }

    public async Task<ShortUserResponse> Execute(LoginUserRequest request)
    {
        Validate(request);

        var user = await _readUserRepository.GetUserByEmail(request.Email) ?? throw new NotFoundException(MessagesException.USER_NOT_FOUND);

        if (user is null || !_passwordEncripter.IsValid(request.Password, user.Password)) throw new InvalidLoginException();

        return _mapper.Map<ShortUserResponse>(user);
    }

    private static void Validate(LoginUserRequest request)
    {
        var result = new LoginUserValidator().Validate(request);

        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}