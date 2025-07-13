namespace TestTask1.Application.DataTransferObjects.Responses;

public record TaskListResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string OwnerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public record TaskListSummaryResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record TaskListUserResponse
{
    public int Id { get; init; }
    public required string UserId { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record PaginatedResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
} 