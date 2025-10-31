namespace Axinar.Application.Tasks;

using Axinar.Application.Contacts;

public record TaskItemDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime DueDate,
    bool IsCompleted,
    Guid? ContactId,
    ContactDto? Contact,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
