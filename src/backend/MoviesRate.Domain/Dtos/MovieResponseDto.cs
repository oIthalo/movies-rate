namespace MoviesRate.Domain.Dtos;

public class MovieResponseDto
{
    public int Id { get; set; }
    public string? Overview { get; set; }
    public string? PosterPath { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Title { get; set; }
}