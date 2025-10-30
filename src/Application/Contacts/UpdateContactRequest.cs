namespace Axinar.Domain.Contacts.Dtos;

public record UpdateContactRequest(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber
);
