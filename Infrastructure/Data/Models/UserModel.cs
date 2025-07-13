using System.ComponentModel.DataAnnotations;

namespace TestTask1.Infrastructure.Data.Models;

public class UserModel
{
    public int Id { get; init; }
    
    [MaxLength(255)]
    public required string UserId { get; init; }
    
    [MaxLength(255)]
    public required string Name { get; init; }
    
    public DateTime CreatedAt { get; init; }
} 