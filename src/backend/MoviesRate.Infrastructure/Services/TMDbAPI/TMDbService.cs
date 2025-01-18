using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Infrastructure.Services.TMDbAPI;

public class TMDbService : ITMDbService
{
    private readonly ITMDbApi _api;

    public TMDbService(ITMDbApi api) =>  _api = api;

    public async Task<MoviesList> GetAllMoviesToDashboard(int page)
    {
        var response = await _api.GetAllMoviesToDashboard(page);
        var randoms = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randoms ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<Movie> GetRandomRecommendedMovieToDashboard()
    {
        var response = await _api.GetRandomRecommendedMovieToDashboard();
        var movie = response.Movies.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();

        return movie!;
    }

    public async Task<MoviesList> Get10RandomTopRatedMovies()
    {
        var response = await _api.GetTopRated();
        var randoms = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randoms ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<MoviesList> Get10RandomPopularMovies()
    {
        var response = await _api.GetPopular();
        var randoms = response.Movies.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randoms ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }
}