namespace Axinar.Application.Tasks;

public record CreateTaskRequest(
    string Title,
    string? Description,
    DateTime DueDate,
    Guid ContactId
);
