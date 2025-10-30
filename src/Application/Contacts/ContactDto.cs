namespace Axinar.Application.Contacts;

using Axinar.Application.Tasks;

public record ContactDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<TaskDto>? Tasks
);
