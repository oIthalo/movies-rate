using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Interfaces;

public interface ITMDbService
{
    Task<MoviesList> Get10RandomTopRatedMovies();
}