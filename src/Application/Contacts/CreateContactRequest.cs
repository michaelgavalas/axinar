namespace Axinar.Domain.Contacts.Dtos;

public record CreateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
