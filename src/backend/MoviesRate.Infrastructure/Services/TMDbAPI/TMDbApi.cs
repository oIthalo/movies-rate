using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Interfaces;
using System.Text.Json;

namespace MoviesRate.Infrastructure.Services.TMDbAPI;

public class TMDbApi : ITMDbApi
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    private const string BASE_URL = "https://api.themoviedb.org/3/";

    public TMDbApi(
        HttpClient httpClient,
        TMDbConfigs config)
    {
        _httpClient = httpClient;
        _apiKey = config.ApiKey;
    }

    public async Task<MoviesList> GetTopRated()
    {
        var endpoint = $"{BASE_URL}movie/top_rated?api_key={_apiKey}";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }
}