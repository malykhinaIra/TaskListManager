using TestTask1.Domain.Entities.Base;

namespace TestTask1.Domain.Entities;

public record User : EntityBase
{
    public required string UserId { get; init; }
    
    public required string Name { get; init; }
} 