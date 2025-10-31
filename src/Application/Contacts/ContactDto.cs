namespace Axinar.Application.Contacts;

using Axinar.Application.Tasks;
using Axinar.Domain.Tasks;

public record ContactDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<TaskItemDto>? Tasks
);
