using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Dashboard.GetAll;

public interface IGetAllDashboardMoviesUseCase
{
    Task<MoviesListResponseDto> Execute(int page);
}