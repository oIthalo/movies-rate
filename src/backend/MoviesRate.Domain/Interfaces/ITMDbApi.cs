using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Interfaces;

public interface ITMDbApi
{
    Task<MoviesList> GetAllMoviesToDashboard(int page);
    Task<MoviesList> GetRandomRecommendedMovieToDashboard();
    Task<MoviesList> GetTopRated();
    Task<MoviesList> GetPopular();
    Task<Movie> GetMovieById(int id);
    Task<GenresList> GetGenres();
    Task<MoviesList> GetMoviesBySearch(string query);
}