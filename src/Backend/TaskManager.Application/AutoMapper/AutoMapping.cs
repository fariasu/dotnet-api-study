using AutoMapper;
using TaskManager.Communication.DTOs.Request;
using TaskManager.Communication.DTOs.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestCreateTaskJson, TaskEntity>();
    }

    private void EntityToResponse()
    {
        CreateMap<TaskEntity, ResponseCreatedTaskJson>();
    }
}