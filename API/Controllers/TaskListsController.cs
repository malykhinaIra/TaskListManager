using Microsoft.AspNetCore.Mvc;
using TestTask1.Application.DTOs;
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
    public async Task<ActionResult<PaginatedResult<TaskListSummaryDto>>> GetTaskLists(
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
    public async Task<ActionResult<TaskListDto>> GetTaskList(int id, [FromHeader(Name = "X-User-Id")] string userId)
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
    public async Task<ActionResult<TaskListDto>> CreateTaskList(
        [FromBody] CreateTaskListDto createTaskListDto,
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

        var result = await _taskListService.CreateTaskListAsync(createTaskListDto, userId);
        return CreatedAtAction(nameof(GetTaskList), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskListDto>> UpdateTaskList(
        int id,
        [FromBody] UpdateTaskListDto updateTaskListDto,
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

        var result = await _taskListService.UpdateTaskListAsync(id, updateTaskListDto, userId);
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
        [FromBody] AddTaskListUserDto addTaskListUserDto,
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

        var result = await _taskListService.AddUserToTaskListAsync(id, addTaskListUserDto, userId);
        if (!result)
        {
            return BadRequest("Unable to add user to task list");
        }

        return Ok();
    }

    [HttpGet("{id}/users")]
    public async Task<ActionResult<IEnumerable<TaskListUserDto>>> GetTaskListUsers(
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