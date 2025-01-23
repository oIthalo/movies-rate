using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Movies.GetMoviesBySearch;

public interface IGetMoviesBySearch
{
    Task<MoviesListResponseDto> Execute(string query);
}