using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using System.Data;

namespace WebAPI.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection _connection = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<MoviesRateDbContextEF>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                // Adicionar o contexto de banco de dados usando a mesma conexão SQLite
                services.AddDbContext<MoviesRateDbContextEF>(opts =>
                {
                    opts.UseSqlite(_connection);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MoviesRateDbContextEF>();

                dbContext.Database.EnsureCreated();
            });
    }

    protected override void Dispose(bool disposing)
    {
        if (_connection.State is not ConnectionState.Closed)
            _connection.Close();
    }
}