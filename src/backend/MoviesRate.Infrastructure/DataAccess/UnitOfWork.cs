using MoviesRate.Domain.Repositories;
using MoviesRate.Infrastructure.DataAccess.DataContexts;

namespace MoviesRate.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly MoviesRateDbContextEF _dbContext;

    public UnitOfWork(MoviesRateDbContextEF dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}