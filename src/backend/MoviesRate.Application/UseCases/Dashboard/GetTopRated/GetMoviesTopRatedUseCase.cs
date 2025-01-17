using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Dashboard.GetTopRated;

public class GetMoviesTopRatedUseCase : IGetMoviesTopRatedUseCase
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public GetMoviesTopRatedUseCase(
        ITMDbService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MoviesListResponseDto> Execute()
    {
        var response = await _service.Get10RandomTopRatedMovies();
        return _mapper.Map<MoviesListResponseDto>(response);
    }
}