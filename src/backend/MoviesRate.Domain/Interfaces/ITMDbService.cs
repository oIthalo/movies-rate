using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Interfaces;

public interface ITMDbService
{
    Task<MoviesList> GetAllMoviesToDashboard(int page);
    Task<Movie> GetRandomRecommendedMovieToDashboard();
    Task<MoviesList> Get10RandomTopRatedMovies();
    Task<MoviesList> Get10RandomPopularMovies();
    Task<Movie> GetMovieById(int id);
}