using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Dashboard.GetPopular;

public class PopularMoviesDashboardUseCase : IPopularMoviesDashboardUseCase
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public PopularMoviesDashboardUseCase(
        ITMDbService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MoviesListResponseDto> Execute()
    {
        var response = await _service.Get10RandomPopularMovies();
        return _mapper.Map<MoviesListResponseDto>(response);
    }
}