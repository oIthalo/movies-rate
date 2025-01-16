using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MoviesRate.Infrastructure.Migrations;

public class DataBaseMigration
{
    protected DataBaseMigration() { }

    public static void Migrate(string connectionStr, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated(connectionStr);
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Database");

        using var sqlConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = sqlConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (!records.Any())
            sqlConnection.Execute($"CREATE DATABASE {databaseName}");
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}