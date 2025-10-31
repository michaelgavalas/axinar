namespace Axinar.Domain.Tasks;

using Axinar.Domain.Contacts;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Contact Foreign key
    public Guid? ContactId { get; set; }
    public Contact? Contact { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
