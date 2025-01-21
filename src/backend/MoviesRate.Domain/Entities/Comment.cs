namespace MoviesRate.Domain.Entities;

public class Comment
{
    public string Text { get; set; } = string.Empty;
    public int Likes { get; set; }
}