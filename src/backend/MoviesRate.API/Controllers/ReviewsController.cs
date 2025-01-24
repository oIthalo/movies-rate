using Microsoft.AspNetCore.Mvc;
using MoviesRate.API.Attributes;
using MoviesRate.Application.UseCases.Reviews.AddReview;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.API.Controllers;

public class ReviewsController : MoviesRateControllerBase
{
    [HttpPost]
    [Route("add-movie-review/{movieId}")]
    [IsAuth]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddReview(
        [FromServices] IAddMovieReviewUseCase useCase,
        [FromBody] ReviewRequest request,
        [FromRoute] int movieId)
    {
        var result = await useCase.Execute(movieId, request);
        return Created(string.Empty, result);
    }
}