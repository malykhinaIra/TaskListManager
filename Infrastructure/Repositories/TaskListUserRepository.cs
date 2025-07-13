using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;
using TestTask1.Infrastructure.Data;

namespace TestTask1.Infrastructure.Repositories;

public class TaskListUserRepository : ITaskListUserRepository
{
    private readonly TaskListDbContext _context;

    public TaskListUserRepository(TaskListDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskListUser>> GetTaskListUsersAsync(int taskListId)
    {
        return await _context.TaskListUsers
            .Where(tlu => tlu.TaskListId == taskListId)
            .ToListAsync();
    }

    public async Task<TaskListUser?> GetTaskListUserAsync(int taskListId, string userId)
    {
        return await _context.TaskListUsers
            .FirstOrDefaultAsync(tlu => tlu.TaskListId == taskListId && tlu.UserId == userId);
    }

    public async Task<TaskListUser> AddAsync(TaskListUser taskListUser)
    {
        _context.TaskListUsers.Add(taskListUser);
        await _context.SaveChangesAsync();
        return taskListUser;
    }

    public async Task<bool> RemoveAsync(int taskListId, string userId)
    {
        var taskListUser = await _context.TaskListUsers
            .FirstOrDefaultAsync(tlu => tlu.TaskListId == taskListId && tlu.UserId == userId);
        
        if (taskListUser == null)
        {
            return false;
        }

        _context.TaskListUsers.Remove(taskListUser);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int taskListId, string userId)
    {
        return await _context.TaskListUsers
            .AnyAsync(tlu => tlu.TaskListId == taskListId && tlu.UserId == userId);
    }
} 