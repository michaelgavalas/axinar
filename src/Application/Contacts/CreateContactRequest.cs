namespace Axinar.Application.Contacts;

public record CreateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
