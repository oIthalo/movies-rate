using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Services.LoggedUser;

namespace MoviesRate.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IWriteUserRepository _writeUserRepository;
    private readonly IReadUserRepository _readUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteUserUseCase(
        IWriteUserRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IReadUserRepository readUserRepository)
    {
        _writeUserRepository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _readUserRepository = readUserRepository;
    }

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.User();

        var user = await _readUserRepository.GetUserByIdentifier(loggedUser.Identifier);

        _writeUserRepository.Delete(user);
        await _unitOfWork.Commit();
    }
}