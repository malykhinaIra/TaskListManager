using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;
using TestTask1.Infrastructure.Data;
using TestTask1.Infrastructure.Data.Models;

namespace TestTask1.Infrastructure.Repositories;

public class TaskListUserRepository : ITaskListUserRepository
{
    private readonly TaskListDbContext _context;
    private readonly IMapper _mapper;

    public TaskListUserRepository(TaskListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskListUser>> GetTaskListUsersAsync(int taskListId)
    {
        var models = await _context.TaskListUsers
            .Where(user => user.TaskListId == taskListId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<TaskListUser>>(models);
    }

    public async Task<TaskListUser?> GetTaskListUserAsync(int taskListId, string userId)
    {
        var model = await _context.TaskListUsers
            .FirstOrDefaultAsync(user => user.TaskListId == taskListId && user.UserId == userId);
        
        return model != null ? _mapper.Map<TaskListUser>(model) : null;
    }

    public async Task<TaskListUser> AddAsync(TaskListUser taskListUser)
    {
        var model = _mapper.Map<TaskListUserModel>(taskListUser);
        
        await _context.TaskListUsers.AddAsync(model);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TaskListUser>(model);
    }

    public async Task<bool> RemoveAsync(int taskListId, string userId)
    {
        var model = await _context.TaskListUsers
            .FirstOrDefaultAsync(user => user.TaskListId == taskListId && user.UserId == userId);
        
        if (model == null)
        {
            return false;
        }

        _context.TaskListUsers.Remove(model);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsAsync(int taskListId, string userId)
    {
        return await _context.TaskListUsers
            .AnyAsync(user => user.TaskListId == taskListId && user.UserId == userId);
    }
} 