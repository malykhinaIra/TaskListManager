using TestTask1.Domain.Entities.Base;

namespace TestTask1.Domain.Entities;

public record TaskListUser : EntityBase
{
    public int TaskListId { get; init; }
    
    public required string UserId { get; init; } 
    
    public TaskList? TaskList { get; init; }
} 