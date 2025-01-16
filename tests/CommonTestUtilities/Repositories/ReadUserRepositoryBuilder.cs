using Moq;
using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class ReadUserRepositoryBuilder
{
    private readonly Mock<IReadUserRepository> _repository;

    public ReadUserRepositoryBuilder() => _repository = new Mock<IReadUserRepository>();

    public IReadUserRepository Build() => _repository.Object;

    public void ExistActiveUserWithEmail(User user) => _repository.Setup(repos => repos.ExistActiveUserWithEmail(user.Email)).ReturnsAsync(true);

    public void GetUserByEmail(User user) => _repository.Setup(repos => repos.GetUserByEmail(user.Email)).ReturnsAsync(user);
}