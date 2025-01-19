using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Repositories.Reviews;

public interface IReadReviewRepository
{
    Task<ReviewsList?> GetReviewByMovieId(int movieId);
}