using System.ComponentModel.DataAnnotations;

namespace TestTask1.Domain.Entities;

public class TaskListUser
{
    public int Id { get; init; }
    
    [Required]
    public int TaskListId { get; init; }
    
    [Required]
    public required string UserId { get; init; } 
    
    public DateTime CreatedAt { get; init; }
    
    public TaskList? TaskList { get; init; }
} 