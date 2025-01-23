using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Movies.GetMovieById;

public interface IGetMovieByIdUseCase
{
    Task<MovieResponseDto> Execute(int id);
}