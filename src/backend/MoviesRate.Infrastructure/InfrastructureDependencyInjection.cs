using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Security.Criptography;
using MoviesRate.Infrastructure.DataAccess;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using MoviesRate.Infrastructure.DataAccess.Repositories.User;
using MoviesRate.Infrastructure.Extensions;
using MoviesRate.Infrastructure.Security.BCryptNet;
using System.Reflection;

namespace MoviesRate.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContexts(services, configuration);
        AddFluentMigrator(services, configuration);
        AddRepositories(services);
        AddPasswordEncripter(services);
    }

    public static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<MoviesRateDbContextEF>(opts => opts.UseSqlServer(connectionString));

        services.AddScoped(opts => new MoviesRateDbContextDapper(connectionString));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(opts =>
        {
            opts.AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MoviesRate.Infrastructure")).For.All();
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReadUserRepository, ReadUserRepository>();
        services.AddScoped<IWriteUserRepository, WriteUserRepository>();
    }

    private static void AddPasswordEncripter(IServiceCollection services) => services.AddScoped<IPasswordEncripter, BCryptNet>();
}