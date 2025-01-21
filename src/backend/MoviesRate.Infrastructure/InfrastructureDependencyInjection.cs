using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesRate.Domain.Interfaces;
using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.Reviews;
using MoviesRate.Domain.Repositories.User;
using MoviesRate.Domain.Security.Criptography;
using MoviesRate.Domain.Security.Tokens.Access;
using MoviesRate.Domain.Security.Tokens.Validator;
using MoviesRate.Domain.Services.LoggedUser;
using MoviesRate.Infrastructure.DataAccess;
using MoviesRate.Infrastructure.DataAccess.DataContexts;
using MoviesRate.Infrastructure.DataAccess.Repositories.Review;
using MoviesRate.Infrastructure.DataAccess.Repositories.User;
using MoviesRate.Infrastructure.Extensions;
using MoviesRate.Infrastructure.Security.BCryptNet;
using MoviesRate.Infrastructure.Security.Tokens.Access.Generator;
using MoviesRate.Infrastructure.Security.Tokens.Validator;
using MoviesRate.Infrastructure.Services.LoggedUser;
using MoviesRate.Infrastructure.Services.TMDbAPI;
using System.Reflection;

namespace MoviesRate.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
        AddRepositories(services);
        AddPasswordEncripter(services);
        AddDbContexts(services, configuration);
        AddTokens(services, configuration);
        AddTMDbApi(services, configuration);
        AddTMDbServices(services);
        AddLoggedUser(services);
    }

    public static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<MoviesRateDbContextEF>(opts => opts.UseSqlServer(connectionString));
        services.AddScoped(opts => new MoviesRateDbContextDapper(connectionString));
    }

    public static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationInMinutes = configuration.GetValue<uint>("Settings:JWT:ExpirationInMinutes");
        var signingKey = configuration.GetValue<string>("Settings:JWT:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(opts => new JwtTokenGenerator(expirationInMinutes, signingKey!));
        services.AddScoped<ITokenValidator>(opts => new TokenValidator(signingKey!));
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

        services.AddScoped<IReadReviewRepository, ReadReviewRepository>();
        services.AddScoped<IWriteReviewRepository, WriteReviewRepository>();
    }

    private static void AddPasswordEncripter(IServiceCollection services) => services.AddScoped<IPasswordEncripter, BCryptNet>();

    private static void AddTMDbApi(IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration.GetValue<string>("Settings:TMDbAPI:ApiKey");

        services.AddHttpClient<ITMDbApi, TMDbApi>(client =>
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        });

        services.AddSingleton(new TMDbConfigs() { ApiKey = apiKey! });
    }

    private static void AddTMDbServices(IServiceCollection services)
    {
        services.AddScoped<ITMDbService, TMDbService>();
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
}