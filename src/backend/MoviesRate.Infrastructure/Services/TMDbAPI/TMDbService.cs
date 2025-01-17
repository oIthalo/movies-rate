using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Infrastructure.Services.TMDbAPI;

public class TMDbService : ITMDbService
{
    private readonly ITMDbApi _api;

    public TMDbService(ITMDbApi api) =>  _api = api;

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
}