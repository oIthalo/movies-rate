using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Reviews.GetMovieById;

public interface IGetMovieByIdUseCase
{
    Task<MovieResponseDto> Execute(int id);
}