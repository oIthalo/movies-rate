using System.Text.Json.Serialization;

namespace MoviesRate.Domain.Entities;

public class Genre
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}