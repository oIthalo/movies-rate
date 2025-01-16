namespace MoviesRate.Communication.Response;

public class ShortUserResponse
{
    public string Name { get; set; } = string.Empty;
    public TokensResponse Tokens { get; set; } = default!
}