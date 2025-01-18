namespace MoviesRate.Domain.Entities;

public class Reviews : EntityBase
{
    public Guid UserIdentifier { get; set; }
    public long MovieId { get; set; }
    public string Comments { get; set; } = string.Empty;
    public decimal Ratings { get; set; }
}