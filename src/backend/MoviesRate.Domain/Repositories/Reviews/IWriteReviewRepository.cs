using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Repositories.Reviews;

public interface IWriteReviewRepository
{
    Task AddReview(Review review);
}