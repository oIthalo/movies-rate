namespace MoviesRate.Domain.Entities;

public class Review : EntityBase
{
    public Guid UserIdentifier { get; set; }
    public int MovieId { get; set; }
    public string Comments { get; set; } = string.Empty;
    public decimal Rating { get; set; }
}