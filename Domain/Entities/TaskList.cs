using System.ComponentModel.DataAnnotations;

namespace TestTask1.Domain.Entities;

public class TaskList
{
    public int Id { get; init; }
    
    [MaxLength(255)]
    public required string Name { get; set; }
    
    public required string OwnerId { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }

    public List<TaskListUser> TaskListUsers { get; init; } = [];
    
    public TaskList JustUpdated()
    {
        UpdatedAt = DateTime.UtcNow;

        return this;
    }
} 