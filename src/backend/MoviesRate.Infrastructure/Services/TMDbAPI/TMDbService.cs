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
        var randomMovies = response.Movies
            .OrderBy(x => Guid.NewGuid())
            .Take(10)
            .ToList();

        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            var movieGenres = genres.Genres
                .Where(x => movie!.GenreIds.Contains(x.Id))
                .ToList();

            movie!.Genres = movieGenres;
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<Movie> GetRandomRecommendedMovieToDashboard()
    {
        var response = await _api.GetRandomRecommendedMovieToDashboard();
        var movie = response.Movies
            .OrderBy(x => Guid.NewGuid())
            .Take(1)
            .FirstOrDefault();

        var genres = await _api.GetGenres();

        var movieGenres = genres.Genres
            .Where(x => movie!.GenreIds.Contains(x.Id))
            .ToList();

        movie!.Genres = movieGenres;

        return movie!;
    }

    public async Task<MoviesList> Get10RandomTopRatedMovies()
    {
        var response = await _api.GetTopRated();
        var randomMovies = response.Movies
            .OrderBy(x => Guid.NewGuid())
            .Take(10)
            .ToList();

        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            var movieGenres = genres.Genres
                .Where(x => movie!.GenreIds.Contains(x.Id))
                .ToList();

            movie!.Genres = movieGenres;
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<MoviesList> Get10RandomPopularMovies()
    {
        var response = await _api.GetPopular();
        var randomMovies = response.Movies
            .OrderBy(x => Guid.NewGuid())
            .Take(10)
            .ToList();

        var genres = await _api.GetGenres();

        foreach (var movie in randomMovies)
        {
            var movieGenres = genres.Genres
                .Where(x => movie!.GenreIds.Contains(x.Id))
                .ToList();

            movie!.Genres = movieGenres;
        }

        return new MoviesList()
        {
            Page = response.Page,
            Movies = randomMovies ?? [],
            TotalPages = response.TotalPages,
            TotalResults = response.TotalResults,
        };
    }

    public async Task<Movie> GetMovieById(int id)
    {
        var response = await _api.GetMovieById(id);
        return response;
    }
}