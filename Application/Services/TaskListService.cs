using AutoMapper;
using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.DataTransferObjects.Responses;
using TestTask1.Domain.Entities;
using TestTask1.Domain.Interfaces;

namespace TestTask1.Application.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly ITaskListUserRepository _taskListUserRepository;
    private readonly IMapper _mapper;

    public TaskListService(ITaskListRepository taskListRepository, ITaskListUserRepository taskListUserRepository, IMapper mapper)
    {
        _taskListRepository = taskListRepository;
        _taskListUserRepository = taskListUserRepository;
        _mapper = mapper;
    }

    public async Task<TaskListResponse?> GetTaskListAsync(int id, string userId)
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

        var taskListEntity = _mapper.Map<TaskList>(taskList);
        return _mapper.Map<TaskListResponse>(taskListEntity);
    }

    public async Task<PaginatedResponse<TaskListSummaryResponse>> GetTaskListsAsync(string userId, int page = 1, int pageSize = 10)
    {
        var (taskLists, totalCount) = await _taskListRepository.GetTaskListsForUserAsync(userId, page, pageSize);
        
        var taskListEntities = _mapper.Map<List<TaskList>>(taskLists);
        var items = _mapper.Map<List<TaskListSummaryResponse>>(taskListEntities);

        return new PaginatedResponse<TaskListSummaryResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<TaskListResponse> CreateTaskListAsync(CreateTaskListRequest request, string userId)
    {
        var taskListEntity = _mapper.Map<TaskList>(request);
        
        taskListEntity = taskListEntity with { OwnerId = userId };
        
        var taskList = _mapper.Map<TaskList>(taskListEntity);
        var createdTaskList = await _taskListRepository.CreateAsync(taskList);
        var createdEntity = _mapper.Map<TaskList>(createdTaskList);
        
        return _mapper.Map<TaskListResponse>(createdEntity);
    }

    public async Task<TaskListResponse?> UpdateTaskListAsync(int id, UpdateTaskListRequest request, string userId)
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

        var taskListEntity = _mapper.Map<TaskList>(taskList);
        var updatedEntity = _mapper.Map(request, taskListEntity);
        var updatedTaskList = _mapper.Map<TaskList>(updatedEntity);
        
        updatedTaskList.UpdatedAt = DateTime.UtcNow;

        var result = await _taskListRepository.UpdateAsync(updatedTaskList);
        var resultEntity = _mapper.Map<TaskList>(result);
        return _mapper.Map<TaskListResponse>(resultEntity);
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

    public async Task<bool> AddUserToTaskListAsync(int taskListId, AddUserToTaskListRequest request, string requestUserId)
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

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            return false;
        }

        if (request.UserId == requestUserId)
        {
            return false;
        }

        var existingRelation = await _taskListUserRepository.ExistsAsync(taskListId, request.UserId);
        if (existingRelation)
        {
            return false;
        }

        var taskListUserEntity = _mapper.Map<TaskListUser>(request);
        taskListUserEntity = taskListUserEntity with { TaskListId = taskListId };
        
        var taskListUser = _mapper.Map<TaskListUser>(taskListUserEntity);

        await _taskListUserRepository.AddAsync(taskListUser);
        return true;
    }

    public async Task<List<TaskListUserResponse>> GetTaskListUsersAsync(int taskListId, string userId)
    {
        var hasAccess = await _taskListRepository.HasAccessToTaskListAsync(taskListId, userId);
        if (!hasAccess)
        {
            return [];
        }

        var taskListUsers = await _taskListUserRepository.GetTaskListUsersAsync(taskListId);
        var taskListUserEntities = _mapper.Map<List<TaskListUser>>(taskListUsers);
        return _mapper.Map<List<TaskListUserResponse>>(taskListUserEntities);
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


} 