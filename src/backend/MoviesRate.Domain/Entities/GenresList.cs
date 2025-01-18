using System.Text.Json.Serialization;

namespace MoviesRate.Domain.Entities;

public class GenresList
{
    [JsonPropertyName("genres")]
    public IList<Genre> Genres { get; set; } = [];
}