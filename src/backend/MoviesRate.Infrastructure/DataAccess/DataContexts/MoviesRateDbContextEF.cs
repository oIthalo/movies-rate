using Microsoft.EntityFrameworkCore;
using MoviesRate.Domain.Entities;

namespace MoviesRate.Infrastructure.DataAccess.DataContexts;

public class MoviesRateDbContextEF : DbContext
{
    public MoviesRateDbContextEF(DbContextOptions opts) : base(opts) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesRateDbContextEF).Assembly);
    }
}