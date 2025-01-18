using AutoMapper;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Interfaces;

namespace MoviesRate.Application.UseCases.Dashboard.GetAll;

public class GetAllMoviesUseCase : IGetAllDashboardMoviesUseCase
{
    private readonly ITMDbService _service;
    private readonly IMapper _mapper;

    public GetAllMoviesUseCase(
        ITMDbService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MoviesListResponseDto> Execute(int page)
    {
        var response = await _service.GetAllMoviesToDashboard(page);
        return _mapper.Map<MoviesListResponseDto>(response);
    }
}