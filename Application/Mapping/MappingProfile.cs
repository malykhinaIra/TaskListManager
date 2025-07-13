using AutoMapper;
using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.DataTransferObjects.Responses;
using TestTask1.Domain.Entities;
using TestTask1.Infrastructure.Data.Models;

namespace TestTask1.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskList, TaskListModel>();
        CreateMap<TaskListModel, TaskList>();
        CreateMap<TaskListUser, TaskListUserModel>();
        CreateMap<TaskListUserModel, TaskListUser>();

        CreateMap<CreateTaskListRequest, TaskList>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TaskListUsers, opt => opt.MapFrom(src => new List<TaskListUser>()));
        
        CreateMap<UpdateTaskListRequest, TaskList>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TaskListUsers, opt => opt.Ignore());

        CreateMap<AddUserToTaskListRequest, TaskListUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<TaskList, TaskListResponse>();
        CreateMap<TaskList, TaskListSummaryResponse>();
        CreateMap<TaskListUser, TaskListUserResponse>();
    }
} 