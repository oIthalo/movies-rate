namespace MoviesRate.Domain.Repositories.User;

public interface IReadUserRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetUserByEmail(string email);
    Task<Entities.User?> GetUserByIdentifier(Guid identifier);
}