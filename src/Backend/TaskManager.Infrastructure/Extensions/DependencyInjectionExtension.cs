using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Infrastructure.DataAccess.Db;
using TaskManager.Infrastructure.DataAccess.Repositories.Tasks;

namespace TaskManager.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITaskRepositoryWriteOnly, TaskRepository>();
        services.AddScoped<ITaskRepositoryReadOnly, TaskRepository>();
        services.AddScoped<ITaskRepositoryUpdateOnly, TaskRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

        services.AddDbContext<TaskManagerDbContext>(config =>
            config.UseMySql(connectionString, serverVersion));
    }

    private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            var connectionString = configuration.ConnectionString();

            options.AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("TaskManager.Infrastructure")).For.All();
        });
    }
}