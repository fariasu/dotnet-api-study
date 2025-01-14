using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Communication.DTOs.Users.Requests;
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
        CreateMap<RequestTaskJson, TaskEntity>();
        CreateMap<RequestUpdateProfileJson, UserEntity>();

        CreateMap<RequestRegisterUserJson, UserEntity>()
            .ForMember(dest 
                => dest.Password, opt 
                => opt.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<TaskEntity, ResponseCreatedTaskJson>();
        CreateMap<TaskEntity, ResponseTaskShortJson>();
        CreateMap<TaskEntity, ResponseTaskJson>();
    }
}