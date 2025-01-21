using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.Application.UseCases.Reviews.AddReview;

public interface IAddMovieReviewUseCase
{
    Task<ReviewResponse> Execute(int movieId, ReviewRequest request);
}