using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Dashboard.GetTopRated;

public interface IGetMoviesTopRatedUseCase
{
    Task<MoviesListResponseDto> Execute();
}