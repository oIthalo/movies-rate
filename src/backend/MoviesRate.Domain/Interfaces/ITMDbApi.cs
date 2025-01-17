using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Interfaces;

public interface ITMDbApi
{
    Task<MoviesList> GetTopRated();
}