namespace Axinar.Application.Tasks.Dtos;

public record TaskSummaryDto(
    Guid Id,
    string Title,
    bool IsCompleted,
    DateTime DueDate,
    string? ContactName,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
