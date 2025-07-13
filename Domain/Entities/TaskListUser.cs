using System.ComponentModel.DataAnnotations;
using TestTask1.Domain.Entities.Base;

namespace TestTask1.Domain.Entities;

public record TaskListUser : EntityBase
{
    [Required]
    public int TaskListId { get; init; }
    
    [Required]
    public required string UserId { get; init; } 
    
    public TaskList? TaskList { get; init; }
} 