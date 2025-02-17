﻿using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.AutoMapper;
using TaskManager.Application.UseCases.Tasks.Create;
using TaskManager.Application.UseCases.Tasks.Delete;
using TaskManager.Application.UseCases.Tasks.GetAll;
using TaskManager.Application.UseCases.Tasks.GetById;
using TaskManager.Application.UseCases.Tasks.Update;
using TaskManager.Application.UseCases.Users.Login;
using TaskManager.Application.UseCases.Users.Register;
using TaskManager.Application.UseCases.Users.Update;
using TaskManager.Application.UseCases.Users.Update.Password;
using TaskManager.Application.UseCases.Users.Update.Profile;

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
        services.AddScoped<IGetAllTasksUseCase, GetAllTasksUseCase>();
        services.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
        services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
        services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
        services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
    }
}