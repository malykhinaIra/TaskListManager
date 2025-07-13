using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestTask1.Infrastructure.Data.Models;

[Table("TaskLists")]
[Index(nameof(OwnerId))]
[Index(nameof(CreatedAt))]
[Index(nameof(OwnerId), nameof(CreatedAt))]
public class TaskListModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [MaxLength(255)]
    public required string Name { get; init; }
    
    public required string OwnerId { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime? UpdatedAt { get; init; }
    
    public List<TaskListUserModel> TaskListUsers { get; init; } = [];
} 