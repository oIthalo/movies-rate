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

    public async Task<Domain.Entities.User?> GetUserByEmail(string email) =>
        (await _dbContext.Connection
            .QueryAsync<Domain.Entities.User>(
                "spGetUserByEmail",
                new { email },
                commandType: CommandType.StoredProcedure))
            .FirstOrDefault();

    public async Task<Domain.Entities.User> GetUserByIdentifier(Guid identifier) =>
        (await _dbContext.Connection
            .QueryAsync<Domain.Entities.User>(
                "spGetUserByIdentifier",
                new { identifier },
                commandType: CommandType.StoredProcedure))
            .First();
}