using MoviesRate.Domain.Repositories.User;
using MoviesRate.Infrastructure.DataAccess.DataContexts;

namespace MoviesRate.Infrastructure.DataAccess.Repositories.User;

public class WriteUserRepository : IWriteUserRepository
{
    private readonly MoviesRateDbContextEF _dbContext;

    public WriteUserRepository(MoviesRateDbContextEF dbContext) => _dbContext = dbContext;

    public async Task Add(Domain.Entities.User user) => await _dbContext.Users.AddAsync(user);

    public void Update(Domain.Entities.User user) => _dbContext.Users.Update(user);
}