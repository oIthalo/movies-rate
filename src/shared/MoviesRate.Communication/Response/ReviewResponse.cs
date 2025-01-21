namespace MoviesRate.Communication.Response;

public class ReviewResponse
{
    public int MovieId { get; set; }
    public string Comments { get; set; } = string.Empty;
    public decimal Rating { get; set; }
}