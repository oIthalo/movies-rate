using Dapper;
using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Repositories.Reviews;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using System.Data;

namespace MoviesRate.Infrastructure.DataAccess.Repositories.Review;

public class ReadReviewRepository : IReadReviewRepository
{
    private readonly MoviesRateDbContextDapper _dbContext;

    public ReadReviewRepository(MoviesRateDbContextDapper dbContext) => _dbContext = dbContext;

    public async Task<ReviewsList?> GetReviewByMovieId(int movieId) =>
        (await _dbContext.Connection
            .QueryAsync<ReviewsList>(
                "spGetReviewByMovieId",
                new { movieId },
                commandType: CommandType.StoredProcedure))
            .FirstOrDefault();
}