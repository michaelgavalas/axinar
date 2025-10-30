namespace Axinar.Application.Tasks;

public record UpdateTaskRequest(
    Guid Id,
    string Title,
    string? Description,
    DateTime DueDate,
    Guid ContactId
);
