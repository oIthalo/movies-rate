using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Dashboard.GetPopular;

public interface IPopularMoviesDashboardUseCase
{
    Task<MoviesListResponseDto> Execute();
}