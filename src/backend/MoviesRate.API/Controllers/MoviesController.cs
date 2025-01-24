using Microsoft.AspNetCore.Mvc;
using MoviesRate.API.Attributes;
using MoviesRate.Application.UseCases.Movies.GetMovieById;
using MoviesRate.Domain.Dtos;

namespace MoviesRate.API.Controllers;

public class MoviesController : MoviesRateControllerBase
{
    [HttpGet]
    [Route("get-movie-by/{id}")]
    [IsAuth]
    [ProducesResponseType(typeof(MovieResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovieById(
        [FromServices] IGetMovieByIdUseCase useCase,
        [FromRoute] int id)
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }
}