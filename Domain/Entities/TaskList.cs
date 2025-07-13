using System.ComponentModel.DataAnnotations;
using TestTask1.Domain.Entities.Base;

namespace TestTask1.Domain.Entities;

public record TaskList : EntityBase
{
    [MaxLength(255)]
    public required string Name { get; init; }
    
    public required string OwnerId { get; init; }
    
    public DateTime? UpdatedAt { get; set; }

    public List<TaskListUser> TaskListUsers { get; init; } = [];
} 