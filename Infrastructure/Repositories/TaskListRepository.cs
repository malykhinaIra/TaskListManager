using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;
using TestTask1.Infrastructure.Data;
using TestTask1.Infrastructure.Data.Models;

namespace TestTask1.Infrastructure.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly TaskListDbContext _context;
    private readonly IMapper _mapper;

    public TaskListRepository(TaskListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskList?> GetByIdAsync(int id)
    {
        var model = await _context.TaskLists.FirstOrDefaultAsync(list => list.Id == id);
        
        return model != null ? _mapper.Map<TaskList>(model) : null;
    }

    public async Task<TaskList?> GetByIdIncludeUsersAsync(int id)
    {
        var model = await _context.TaskLists
            .Include(list => list.TaskListUsers)
            .FirstOrDefaultAsync(list => list.Id == id);
        
        return model != null ? _mapper.Map<TaskList>(model) : null;
    }

    public async Task<(List<TaskList> TaskLists, int TotalCount)> GetTaskListsForUserAsync(string userId, int page, int pageSize)
    {
        var query = _context.TaskLists
            .Where(model => model.OwnerId == userId || model.TaskListUsers.Any(user => user.UserId == userId));

        var totalCount = await query.CountAsync();
        
        var models = await query
            .OrderByDescending(model => model.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (_mapper.Map<List<TaskList>>(models), totalCount);
    }

    public async Task<TaskList> CreateAsync(TaskList taskList)
    {
        var model = _mapper.Map<TaskListModel>(taskList);
        
        await _context.TaskLists.AddAsync(model);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TaskList>(model);
    }

    public async Task<TaskList> UpdateAsync(TaskList taskList)
    {
        var model = _mapper.Map<TaskListModel>(taskList);
        
        _context.TaskLists.Update(model);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TaskList>(model);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _context.TaskLists.FindAsync(id);
        if (model == null)
        {
            return false;
        }

        _context.TaskLists.Remove(model);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> HasAccessToTaskListAsync(int taskListId, string userId)
    {
        return await _context.TaskLists
            .AnyAsync(model => model.Id == taskListId && 
                           (model.OwnerId == userId || model.TaskListUsers.Any(user => user.UserId == userId)));
    }

    public async Task<bool> IsOwnerAsync(int taskListId, string userId)
    {
        return await _context.TaskLists.AnyAsync(model => model.Id == taskListId && model.OwnerId == userId);
    }
} 