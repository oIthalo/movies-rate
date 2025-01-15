using AutoMapper;
using FluentValidation.Results;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IReadUserRepository _readUserRepository;
    private readonly IWriteUserRepository _writeUserRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IReadUserRepository readUserRepository, 
        IWriteUserRepository writeUserRepository, 
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _readUserRepository = readUserRepository;
        _writeUserRepository = writeUserRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ShortUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        await _writeUserRepository.Add(user);
        await _unitOfWork.Commit();

        return new ShortUserResponse()
        {
            Name = user.Name,
        };
    }

    private async Task Validate(RegisterUserRequest request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var existUser = await _readUserRepository.ExistActiveUserWithEmail(request.Email);
        if (existUser) result.Errors.Add(new ValidationFailure(string.Empty, MessagesException.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}