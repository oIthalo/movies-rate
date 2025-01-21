using MoviesRate.Domain.Repositories.Reviews;
using MoviesRate.Infrastructure.DataAccess.DataContexts;

namespace MoviesRate.Infrastructure.DataAccess.Repositories.Review;

public class WriteReviewRepository : IWriteReviewRepository
{
    private readonly MoviesRateDbContextEF _dbContext;

    public WriteReviewRepository(MoviesRateDbContextEF dbContext) => _dbContext = dbContext;

    public async Task AddReview(Domain.Entities.Review review) => await _dbContext.Reviews.AddAsync(review);
}