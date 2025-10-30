namespace Axinar.Domain.Contacts;

using Axinar.Domain.Tasks;

public class Contact
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Tasks List
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
