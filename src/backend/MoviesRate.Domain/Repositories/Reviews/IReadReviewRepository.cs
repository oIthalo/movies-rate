using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Repositories.Reviews;

public interface IReadReviewRepository
{
    Task<List<Review>?> GetReviewsByMovieId(int movieId);
}