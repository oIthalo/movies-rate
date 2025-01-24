using FluentValidation.Results;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Services.LoggedUser;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IReadUserRepository _readUserRepository;
    private readonly IWriteUserRepository _writeUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateUserUseCase(
        IReadUserRepository readUserRepository, 
        IWriteUserRepository writeUserRepository, 
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser)
    {
        _readUserRepository = readUserRepository;
        _writeUserRepository = writeUserRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(UpdateUserRequest request)
    {
        await Validate(request);

        var loggedUser = await _loggedUser.User();

        var user = await _readUserRepository.GetUserByIdentifier(loggedUser.Identifier);
        user.Name = request.Name;
        user.Email = request.Email;

        _writeUserRepository.Update(user);
        await _unitOfWork.Commit();

        return new UpdateUserResponse()
        {
            Name = user.Name,
            Email = user.Email,
        };
    }

    private async Task Validate(UpdateUserRequest request)
    {
        var result = new UpdateUserValidator().Validate(request);

        var alreadyExist = await _readUserRepository.ExistActiveUserWithEmail(request.Email);
        if (alreadyExist) result.Errors.Add(new ValidationFailure(string.Empty, MessagesException.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}