using System.Net.Http.Json;

namespace WebAPI.Test;

public class MoviesRateClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public MoviesRateClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(string method, object request)
    {
        return await _httpClient.PostAsJsonAsync(method, request);
    }
}