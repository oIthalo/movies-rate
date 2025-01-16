using Dapper;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using System.Data;

namespace MoviesRate.Infrastructure.DataAccess.Repositories.User;

public class ReadUserRepository : IReadUserRepository
{
    private readonly MoviesRateDbContextDapper _dbContext;

    public ReadUserRepository(MoviesRateDbContextDapper dbContext) => _dbContext = dbContext;

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        (await _dbContext.Connection
               .QueryAsync<bool>(
                   "spExistActiveUserWithEmail",
                   new { email },
                   commandType: CommandType.StoredProcedure))
                .FirstOrDefault();
}