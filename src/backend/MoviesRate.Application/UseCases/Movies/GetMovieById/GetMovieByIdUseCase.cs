using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Movies.GetMovieById;

public class GetMovieByIdUseCase : IGetMovieByIdUseCase
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public GetMovieByIdUseCase(
        ITMDbService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MovieResponseDto> Execute(int id)
    {
        var response = await _service.GetMovieById(id);
        return _mapper.Map<MovieResponseDto>(response);
    }
}