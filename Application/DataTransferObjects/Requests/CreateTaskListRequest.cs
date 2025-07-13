using System.ComponentModel.DataAnnotations;

namespace TestTask1.Application.DataTransferObjects.Requests;

public record CreateTaskListRequest
{
    [MaxLength(255)]
    public required string Name { get; init; }
}

public record UpdateTaskListRequest
{
    [MaxLength(255)]
    public required string Name { get; init; }
}

public record AddUserToTaskListRequest
{
    public required string UserId { get; init; }
} 