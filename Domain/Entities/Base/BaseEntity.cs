namespace TestTask1.Domain.Entities.Base;

public abstract record EntityBase
{
    public int Id { get; init; }
    
    public DateTime CreatedAt { get; init; }
}