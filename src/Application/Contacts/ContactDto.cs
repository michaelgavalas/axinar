namespace Axinar.Domain.Contacts.Dtos;

public record ContactDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

// TODO: Add when TaskDto is implemented
// IEnumerable<TaskDto>? Tasks
