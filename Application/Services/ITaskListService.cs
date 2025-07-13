using TestTask1.Application.DTOs;

namespace TestTask1.Application.Services;

public interface ITaskListService
{
    Task<TaskListDto?> GetTaskListAsync(int id, string userId);
    Task<PaginatedResult<TaskListSummaryDto>> GetTaskListsAsync(string userId, int page = 1, int pageSize = 10);
    Task<TaskListDto> CreateTaskListAsync(CreateTaskListDto createTaskListDto, string userId);
    Task<TaskListDto?> UpdateTaskListAsync(int id, UpdateTaskListDto updateTaskListDto, string userId);
    Task<bool> DeleteTaskListAsync(int id, string userId);
    Task<bool> AddUserToTaskListAsync(int taskListId, AddTaskListUserDto addTaskListUserDto, string requestUserId);
    Task<List<TaskListUserDto>> GetTaskListUsersAsync(int taskListId, string userId);
    Task<bool> RemoveUserFromTaskListAsync(int taskListId, string userIdToRemove, string requestUserId);
} 