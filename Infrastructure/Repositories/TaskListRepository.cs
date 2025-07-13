using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;
using TestTask1.Infrastructure.Data;

namespace TestTask1.Infrastructure.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly TaskListDbContext _context;

    public TaskListRepository(TaskListDbContext context)
    {
        _context = context;
    }

    public async Task<TaskList?> GetByIdAsync(int id)
    {
        return await _context.TaskLists.FirstOrDefaultAsync(tl => tl.Id == id);
    }

    public async Task<TaskList?> GetByIdIncludeUsersAsync(int id)
    {
        return await _context.TaskLists
            .Include(tl => tl.TaskListUsers)
            .FirstOrDefaultAsync(tl => tl.Id == id);
    }

    public async Task<(IEnumerable<TaskList> TaskLists, int TotalCount)> GetTaskListsForUserAsync(string userId, int page, int pageSize)
    {
        var query = _context.TaskLists
            .Where(tl => tl.OwnerId == userId || tl.TaskListUsers.Any(tlu => tlu.UserId == userId));

        var totalCount = await query.CountAsync();
        var taskLists = await query
            .OrderByDescending(tl => tl.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (taskLists, totalCount);
    }

    public async Task<TaskList> CreateAsync(TaskList taskList)
    {
        _context.TaskLists.Add(taskList);
        
        await _context.SaveChangesAsync();
        
        return taskList;
    }

    public async Task<TaskList> UpdateAsync(TaskList taskList)
    {
        _context.TaskLists.Update(taskList);

        await _context.SaveChangesAsync();
        
        return taskList;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var taskList = await _context.TaskLists.FindAsync(id);
        if (taskList == null)
        {
            return false;
        }

        _context.TaskLists.Remove(taskList);
        
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> HasAccessToTaskListAsync(int taskListId, string userId)
    {
        return await _context.TaskLists
            .AnyAsync(tl => tl.Id == taskListId && 
                           (tl.OwnerId == userId || tl.TaskListUsers.Any(tlu => tlu.UserId == userId)));
    }

    public async Task<bool> IsOwnerAsync(int taskListId, string userId)
    {
        return await _context.TaskLists
            .AnyAsync(tl => tl.Id == taskListId && tl.OwnerId == userId);
    }
} 