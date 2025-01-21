namespace MoviesRate.Communication.Requests;

public class ReviewRequest
{
    public string Comments { get; set; } = string.Empty;
    public decimal Rating { get; set; }
}