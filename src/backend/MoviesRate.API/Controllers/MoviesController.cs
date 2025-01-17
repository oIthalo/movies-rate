using Microsoft.AspNetCore.Mvc;
using MoviesRate.Application.UseCases.Dashboard.GetTopRated;
using MoviesRate.Domain.Dtos;

namespace MoviesRate.API.Controllers;

public class MoviesController : MoviesRateControllerBase
{
    [HttpGet]
    [Route("dashboard-top-rated")]
    [ProducesResponseType(typeof(MoviesListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get10RandomTopRatedMovies(
        [FromServices] IGetMoviesTopRatedUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }
}