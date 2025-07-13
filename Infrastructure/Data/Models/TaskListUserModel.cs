using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestTask1.Infrastructure.Data.Models;

[Table("TaskListUsers")]
[Index(nameof(TaskListId), nameof(UserId), IsUnique = true)]
[Index(nameof(TaskListId))]
[Index(nameof(UserId))]
[Index(nameof(CreatedAt))]
public class TaskListUserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    public int TaskListId { get; init; }
    
    public required string UserId { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    [ForeignKey(nameof(TaskListId))]
    public TaskListModel? TaskList { get; init; }
} 