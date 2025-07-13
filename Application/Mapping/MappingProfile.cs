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
        CreateMap<TaskListModel, TaskList>();
        CreateMap<TaskList, TaskListModel>();
        CreateMap<CreateTaskListRequest, TaskList>();
        CreateMap<TaskList, TaskListResponse>();
        CreateMap<TaskList, TaskListSummaryResponse>();
        CreateMap<UpdateTaskListRequest, TaskList>();

        CreateMap<TaskListUserModel, TaskListUser>();
        CreateMap<TaskListUser, TaskListUserModel>();
        CreateMap<AddUserToTaskListRequest, TaskListUser>();
        CreateMap<TaskListUser, TaskListUserResponse>();

        CreateMap<UserModel, User>();
        CreateMap<User, UserModel>();
        CreateMap<CreateUserRequest, User>();
    }
} 