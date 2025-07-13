namespace TestTask1.Application.DataTransferObjects.Requests;

public record CreateUserRequest
{
    public required string UserId { get; init; }
    
    public required string Name { get; init; }
} 