using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> User();
}