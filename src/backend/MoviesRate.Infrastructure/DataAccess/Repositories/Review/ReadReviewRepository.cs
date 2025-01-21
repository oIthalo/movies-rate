using Dapper;
using MoviesRate.Domain.Repositories.Reviews;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using System.Data;

namespace MoviesRate.Infrastructure.DataAccess.Repositories.Review;

public class ReadReviewRepository : IReadReviewRepository
{
    private readonly MoviesRateDbContextDapper _dbContext;

    public ReadReviewRepository(MoviesRateDbContextDapper dbContext) => _dbContext = dbContext;

    public async Task<List<Domain.Entities.Review>?> GetReviewsByMovieId(int movieId)
    {
        var reviews = await _dbContext.Connection
            .QueryAsync<Domain.Entities.Review>(
                "spGetReviewsByMovieId",
                new { MovieId = movieId },
                commandType: CommandType.StoredProcedure);

        return reviews.ToList();
    }
}