using MoviesRate.Domain.Entities;

namespace MoviesRate.Domain.Dtos;

public class MovieResponseDto
{
    public int Id { get; set; }
    public string? Overview { get; set; }
    public string? PosterPath { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Title { get; set; }
    public List<Genre> Genres { get; set; } = [];
    public decimal NoteAverage { get; set; }
}