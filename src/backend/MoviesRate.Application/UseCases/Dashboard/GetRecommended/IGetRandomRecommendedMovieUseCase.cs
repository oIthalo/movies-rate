using MoviesRate.Domain.Dtos;

namespace MoviesRate.Application.UseCases.Dashboard.GetRecommended;

public interface IGetRandomRecommendedMovieUseCase
{
    Task<MovieResponseDto> Execute();
}