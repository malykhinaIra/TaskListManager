using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.DataTransferObjects.Responses;

namespace TestTask1.Application.Services.Interfaces;

public interface ITaskListService
{
    Task<TaskListResponse?> GetTaskListAsync(int id, string userId);
    Task<PaginatedResponse<TaskListSummaryResponse>> GetTaskListsAsync(string userId, int page = 1, int pageSize = 10);
    Task<TaskListResponse> CreateTaskListAsync(CreateTaskListRequest request, string userId);
    Task<TaskListResponse?> UpdateTaskListAsync(int id, UpdateTaskListRequest request, string userId);
    Task<bool> DeleteTaskListAsync(int id, string userId);
    Task<bool> AddUserToTaskListAsync(int taskListId, AddUserToTaskListRequest request, string requestUserId);
    Task<List<TaskListUserResponse>> GetTaskListUsersAsync(int taskListId, string userId);
    Task<bool> RemoveUserFromTaskListAsync(int taskListId, string userIdToRemove, string requestUserId);
} 