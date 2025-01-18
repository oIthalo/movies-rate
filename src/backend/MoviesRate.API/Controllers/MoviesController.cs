using Microsoft.AspNetCore.Mvc;
using MoviesRate.Application.UseCases.Reviews.GetMovieById;
using MoviesRate.Domain.Dtos;

namespace MoviesRate.API.Controllers;

public class MoviesController : MoviesRateControllerBase
{
    [HttpGet]
    [Route("get-movie-by/{id}")]
    [ProducesResponseType(typeof(MovieResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovieById(
        [FromServices] IGetMovieByIdUseCase useCase,
        [FromRoute] int id)
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }
}