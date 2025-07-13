using System.ComponentModel.DataAnnotations;

namespace TestTask1.Application.DTOs;

public class TaskListDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string OwnerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public class TaskListSummaryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; init; }
}

public class CreateTaskListDto
{
    [MaxLength(255)]
    public required string Name { get; init; }
}

public class UpdateTaskListDto
{
    [MaxLength(255)]
    public required string Name { get; init; }
}

public class TaskListUserDto
{
    public int Id { get; init; }
    public required string UserId { get; init; }
    public DateTime CreatedAt { get; init; }
}

public class AddTaskListUserDto
{
    public required string UserId { get; init; }
}

public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
} 