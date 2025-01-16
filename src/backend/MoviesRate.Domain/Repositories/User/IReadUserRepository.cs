namespace MoviesRate.Domain.Repositories.User;

public interface IReadUserRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
}