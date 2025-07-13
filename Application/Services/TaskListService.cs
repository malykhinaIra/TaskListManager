using TestTask1.Application.DTOs;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;

namespace TestTask1.Application.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly ITaskListUserRepository _taskListUserRepository;

    public TaskListService(ITaskListRepository taskListRepository, ITaskListUserRepository taskListUserRepository)
    {
        _taskListRepository = taskListRepository;
        _taskListUserRepository = taskListUserRepository;
    }

    public async Task<TaskListDto?> GetTaskListAsync(int id, string userId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(id, userId);
        if (!hasAccess)
        {
            return null;
        }

        var taskList = await _taskListRepository.GetByIdAsync(id);
        if (taskList == null)
        {
            return null;
        }

        return MapToDto(taskList);
    }

    public async Task<PaginatedResult<TaskListSummaryDto>> GetTaskListsAsync(string userId, int page = 1, int pageSize = 10)
    {
        var (taskLists, totalCount) = await _taskListRepository.GetTaskListsForUserAsync(userId, page, pageSize);
        
        var items = taskLists.Select(tl => new TaskListSummaryDto
        {
            Id = tl.Id,
            Name = tl.Name,
            CreatedAt = tl.CreatedAt
        }).ToList();

        return new PaginatedResult<TaskListSummaryDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto createTaskListDto, string userId)
    {
        var taskList = new TaskList
        {
            Name = createTaskListDto.Name,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };

        var createdTaskList = await _taskListRepository.CreateAsync(taskList);
        return MapToDto(createdTaskList);
    }

    public async Task<TaskListDto?> UpdateTaskListAsync(int id, UpdateTaskListDto updateTaskListDto, string userId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(id, userId);
        if (!hasAccess)
        {
            return null;
        }

        var taskList = await _taskListRepository.GetByIdAsync(id);
        if (taskList == null)
        {
            return null;
        }

        taskList.Name = updateTaskListDto.Name;
        taskList.JustUpdated();

        var updatedTaskList = await _taskListRepository.UpdateAsync(taskList);
        return MapToDto(updatedTaskList);
    }

    public async Task<bool> DeleteTaskListAsync(int id, string userId)
    {
        var isOwner = await _taskListRepository.IsOwnerAsync(id, userId);
        if (!isOwner)
        {
            return false;
        }

        return await _taskListRepository.DeleteAsync(id);
    }

    public async Task<bool> AddUserToTaskListAsync(int taskListId, AddTaskListUserDto addTaskListUserDto, string requestUserId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(taskListId, requestUserId);
        if (!hasAccess)
        {
            return false;
        }

        var taskListExists = await _taskListRepository.GetByIdAsync(taskListId) != null;
        if (!taskListExists)
        {
            return false;
        }

        var existingRelation = await _taskListUserRepository.ExistsAsync(taskListId, addTaskListUserDto.UserId);
        if (existingRelation)
        {
            return false;
        }

        var taskListUser = new TaskListUser
        {
            TaskListId = taskListId,
            UserId = addTaskListUserDto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _taskListUserRepository.AddAsync(taskListUser);
        return true;
    }

    public async Task<List<TaskListUserDto>> GetTaskListUsersAsync(int taskListId, string userId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(taskListId, userId);
        if (!hasAccess)
        {
            return [];
        }

        var taskListUsers = await _taskListUserRepository.GetTaskListUsersAsync(taskListId);
        return taskListUsers.Select(tlu => new TaskListUserDto
        {
            Id = tlu.Id,
            UserId = tlu.UserId,
            CreatedAt = tlu.CreatedAt
        }).ToList();
    }

    public async Task<bool> RemoveUserFromTaskListAsync(int taskListId, string userIdToRemove, string requestUserId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(taskListId, requestUserId);
        if (!hasAccess)
        {
            return false;
        }

        return await _taskListUserRepository.RemoveAsync(taskListId, userIdToRemove);
    }

    private static TaskListDto MapToDto(TaskList taskList)
    {
        return new TaskListDto
        {
            Id = taskList.Id,
            Name = taskList.Name,
            OwnerId = taskList.OwnerId,
            CreatedAt = taskList.CreatedAt,
            UpdatedAt = taskList.UpdatedAt
        };
    }
} 