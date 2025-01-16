using Moq;
using MoviesRate.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class WriteUserRepositoryBuilder
{
    public static IWriteUserRepository Build() => new Mock<IWriteUserRepository>().Object;
}