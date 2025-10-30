namespace Axinar.Application.Contacts;

public record UpdateContactRequest(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber
);
