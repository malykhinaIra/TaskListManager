using TestTask1.Domain.Entities;

namespace TestTask1.Domain.Interfaces;

public interface ITaskListUserRepository
{
    Task<IEnumerable<TaskListUser>> GetTaskListUsersAsync(int taskListId);
    Task<TaskListUser?> GetTaskListUserAsync(int taskListId, string userId);
    Task<TaskListUser> AddAsync(TaskListUser taskListUser);
    Task<bool> RemoveAsync(int taskListId, string userId);
    Task<bool> ExistsAsync(int taskListId, string userId);
} 