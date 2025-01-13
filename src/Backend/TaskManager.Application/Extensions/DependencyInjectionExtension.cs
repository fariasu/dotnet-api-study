using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.AutoMapper;
using TaskManager.Application.UseCases.Create;

namespace TaskManager.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
    }
}