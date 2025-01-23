using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Interfaces;
using MoviesRate.Exception;
using MoviesRate.Exception.Exceptions;
using System.Net;
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

    public async Task<MoviesList> GetAllMoviesToDashboard(int page)
    {
        var endpoint = $"{BASE_URL}movie/popular?api_key={_apiKey}&language=pt-BR&page={page}";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }

    public async Task<MoviesList> GetRandomRecommendedMovieToDashboard()
    {
        var page = new Random().Next(0, 497);

        var endpoint = $"{BASE_URL}movie/popular?api_key={_apiKey}&language=pt-BR&page={page}";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }

    public async Task<MoviesList> GetTopRated()
    {
        var endpoint = $"{BASE_URL}movie/top_rated?api_key={_apiKey}&language=pt-BR";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }

    public async Task<MoviesList> GetPopular()
    {
        var endpoint = $"{BASE_URL}movie/popular?api_key={_apiKey}&language=pt-BR";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }

    public async Task<Movie> GetMovieById(int id)
    {

        var endpoint = $"{BASE_URL}movie/{id}?api_key={_apiKey}&language=pt-BR";
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode is HttpStatusCode.NotFound)
                throw new NotFoundException(MessagesException.MOVIE_NOT_FOUND);
        }

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<Movie>(json);

        return result!;

    }

    public async Task<GenresList> GetGenres()
    {
        var endpoint = $"{BASE_URL}genre/movie/list?api_key={_apiKey}&language=pt-BR";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<GenresList>(json);

        return result!;
    }

    public async Task<MoviesList> GetMoviesBySearch(string query)
    {
        var endpoint = $"{BASE_URL}search/movie?api_key={_apiKey}&language=pt-BR&query={query}";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<MoviesList>(json);

        return result!;
    }
}