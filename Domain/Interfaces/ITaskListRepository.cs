using TestTask1.Domain.Entities;

namespace TestTask1.Domain.Interfaces;

public interface ITaskListRepository
{
    Task<TaskList?> GetByIdAsync(int id);
    Task<TaskList?> GetByIdIncludeUsersAsync(int id);
    Task<(IEnumerable<TaskList> TaskLists, int TotalCount)> GetTaskListsForUserAsync(string userId, int page, int pageSize);
    Task<TaskList> CreateAsync(TaskList taskList);
    Task<TaskList> UpdateAsync(TaskList taskList);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasAccessToTaskListAsync(int taskListId, string userId);
    Task<bool> IsOwnerAsync(int taskListId, string userId);
} 