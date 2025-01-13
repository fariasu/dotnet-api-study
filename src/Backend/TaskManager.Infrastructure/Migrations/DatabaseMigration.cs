using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace TaskManager.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(IServiceProvider serviceProvider, string connectionString)
    {
        EnsureDatabaseCreated_MySql(connectionString);
        
        MigrateDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated_MySql(string connectionString)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
        
        var databaseName = connectionStringBuilder.Database;

        connectionStringBuilder.Remove("Database");
        
        using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = dbConnection.Query("SELECT * FROM information_schema.schemata WHERE SCHEMA_NAME = @name", parameters);

        if (records.Any() == false)
        {
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
    
    private static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}