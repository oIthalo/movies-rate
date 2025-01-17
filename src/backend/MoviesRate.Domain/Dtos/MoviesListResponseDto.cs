namespace MoviesRate.Domain.Dtos;

public class MoviesListResponseDto
{
    public IList<MovieResponseDto> Movies { get; set; } = [];
}