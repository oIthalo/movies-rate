using Microsoft.AspNetCore.Mvc;
using MoviesRate.Application.UseCases.Dashboard.GetAll;
using MoviesRate.Application.UseCases.Dashboard.GetPopular;
using MoviesRate.Application.UseCases.Dashboard.GetRecommended;
using MoviesRate.Application.UseCases.Dashboard.GetTopRated;
using MoviesRate.Domain.Dtos;

namespace MoviesRate.API.Controllers;

public class DashboardController : MoviesRateControllerBase
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

    [HttpGet]
    [Route("dashboard-popular")]
    [ProducesResponseType(typeof(MoviesListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get10RandomPopularMovies(
        [FromServices] IPopularMoviesDashboardUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }

    [HttpGet]
    [Route("get-all-dashboard-movies/{page}")]
    [ProducesResponseType(typeof(MoviesListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDashboardMovies(
        [FromServices] IGetAllDashboardMoviesUseCase useCase,
        [FromRoute] int page = 1)
    {
        var result = await useCase.Execute(page);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-random-recommended-movie")]
    [ProducesResponseType(typeof(MovieResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRandomRecommendedMovieDashboard(
        [FromServices] IGetRandomRecommendedMovieUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }
}