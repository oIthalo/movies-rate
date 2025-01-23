using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Movies.GetMoviesBySearch;

public class GetMoviesBySearch : IGetMoviesBySearch
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public GetMoviesBySearch(
        ITMDbService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MoviesListResponseDto> Execute(string query)
    {
        var response = await _service.GetMoviesBySearch(query);
        return _mapper.Map<MoviesListResponseDto>(response);
    }
}