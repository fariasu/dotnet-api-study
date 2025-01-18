using System.Reflection;
using System.Text;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Security.Cryptography;
using TaskManager.Domain.Security.Tokens;
using TaskManager.Domain.Services;
using TaskManager.Infrastructure.DataAccess.Db;
using TaskManager.Infrastructure.DataAccess.Repositories.Tasks;
using TaskManager.Infrastructure.DataAccess.Repositories.Users;
using TaskManager.Infrastructure.Security.Cryptography;
using TaskManager.Infrastructure.Security.Tokens;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
        AddAuthentication(services, configuration);
        AddRateLimiting(services);
        
        services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<ILoggedUserService, LoggedUserService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITaskRepositoryWriteOnly, TaskRepository>();
        services.AddScoped<ITaskRepositoryReadOnly, TaskRepository>();
        services.AddScoped<ITaskRepositoryUpdateOnly, TaskRepository>();
        
        services.AddScoped<IUserRepositoryWriteOnly, UserRepository>();
        services.AddScoped<IUserRepositoryReadOnly, UserRepository>();
        services.AddScoped<IUserRepositoryUpdateOnly, UserRepository>();
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

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = new TimeSpan(0),
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetValue<string>("Settings:Jwt:SecretKey")!))
            };
        });
    }

    private static void AddRateLimiting(IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("FixedPolicy", limiterOptions =>
            {
                limiterOptions.PermitLimit = 999;
                limiterOptions.Window = TimeSpan.FromSeconds(20);
            });

            options.OnRejected = (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return new ValueTask(context.HttpContext.Response.WriteAsync("You have exceeded the rate limit!", token));
            };
        });
    }
}