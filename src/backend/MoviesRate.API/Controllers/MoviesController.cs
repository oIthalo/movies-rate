using Microsoft.AspNetCore.Mvc;
using MoviesRate.API.Attributes;
using MoviesRate.Application.UseCases.Reviews.AddReview;
using MoviesRate.Application.UseCases.Reviews.GetMovieById;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Dtos;

namespace MoviesRate.API.Controllers;

public class MoviesController : MoviesRateControllerBase
{
    [HttpGet]
    [Route("get-movie-by/{id}")]
    // [IsAuth]
    [ProducesResponseType(typeof(MovieResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovieById(
        [FromServices] IGetMovieByIdUseCase useCase,
        [FromRoute] int id)
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }

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