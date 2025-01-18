using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Dashboard.GetRecommended;

public class GetRandomRecommendedMovieUseCase : IGetRandomRecommendedMovieUseCase
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public GetRandomRecommendedMovieUseCase(
        ITMDbService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MovieResponseDto> Execute()
    {
        var response = await _service.GetRandomRecommendedMovieToDashboard();
        return _mapper.Map<MovieResponseDto>(response);
    }
}