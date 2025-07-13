using Microsoft.AspNetCore.Mvc;
using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.DataTransferObjects.Responses;
using TestTask1.Application.Services;

namespace TestTask1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskListsController : ControllerBase
{
    private readonly ITaskListService _taskListService;

    public TaskListsController(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<TaskListSummaryResponse>>> GetTaskLists(
        [FromHeader(Name = "X-User-Id")] string userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _taskListService.GetTaskListsAsync(userId, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskListResponse>> GetTaskList(int id, [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _taskListService.GetTaskListAsync(id, userId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskListResponse>> CreateTaskList(
        [FromBody] CreateTaskListRequest request,
        [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _taskListService.CreateTaskListAsync(request, userId);
        
        return CreatedAtAction(nameof(GetTaskList), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskListResponse>> UpdateTaskList(
        int id,
        [FromBody] UpdateTaskListRequest request,
        [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _taskListService.UpdateTaskListAsync(id, request, userId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskList(int id, [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _taskListService.DeleteTaskListAsync(id, userId);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/users")]
    public async Task<ActionResult> AddUserToTaskList(
        int id,
        [FromBody] AddUserToTaskListRequest request,
        [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            return BadRequest("Target user ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _taskListService.AddUserToTaskListAsync(id, request, userId);
        if (!result)
        {
            return BadRequest("Unable to add user to task list. User may not exist or already be added.");
        }

        return Ok();
    }

    [HttpGet("{id}/users")]
    public async Task<ActionResult<List<TaskListUserResponse>>> GetTaskListUsers(
        int id,
        [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _taskListService.GetTaskListUsersAsync(id, userId);
        return Ok(result);
    }

    [HttpDelete("{id}/users/{userIdToRemove}")]
    public async Task<ActionResult> RemoveUserFromTaskList(
        int id,
        string userIdToRemove,
        [FromHeader(Name = "X-User-Id")] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _taskListService.RemoveUserFromTaskListAsync(id, userIdToRemove, userId);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
} 