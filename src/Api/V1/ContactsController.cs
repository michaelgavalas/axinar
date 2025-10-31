namespace Axinar.Api.V1;

using Axinar.Application.Contacts;
using Axinar.Application.Tasks;
using Axinar.Domain.Contacts;
using Axinar.Domain.Tasks;
using Axinar.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v1/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContactsController(AppDbContext db) => _db = db;

    // GET: api/v1/contacts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll()
    {
        var contacts = await _db.Contacts
            .AsNoTracking()
            .Include(c => c.Tasks)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(contacts.Select(ToReadDto));
    }

    // GET: api/v1/contacts/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContactDto>> GetById(Guid id)
    {
        var contact = await _db.Contacts
            .AsNoTracking()
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id);

        return contact is null ? NotFound() : Ok(ToReadDto(contact));
    }

    // POST: api/v1/contacts
    [HttpPost]
    public async Task<ActionResult<ContactDto>> Create(CreateContactRequest dto)
    {
        var entity = new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Tasks = new List<TaskItem>()
        };

        _db.Contacts.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, ToReadDto(entity));
    }

    // PUT: api/v1/contacts/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ContactDto>> Update(Guid id, UpdateContactRequest dto)
    {
        if (id != dto.Id) return BadRequest();

        var entity = await _db.Contacts.Include(c => c.Tasks).FirstOrDefaultAsync(c => c.Id == id);
        if (entity is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.FirstName))
            entity.FirstName = dto.FirstName;

        if (!string.IsNullOrWhiteSpace(dto.LastName))
            entity.LastName = dto.LastName;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            entity.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            entity.PhoneNumber = dto.PhoneNumber;

        await _db.SaveChangesAsync();

        return Ok(ToReadDto(entity));
    }

    // DELETE: api/v1/contacts/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _db.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        if (entity is null) return NotFound();

        _db.Contacts.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // Mapping
    private static ContactDto ToReadDto(Contact c) =>
        new(
            c.Id,
            c.FirstName,
            c.LastName,
            c.Email,
            c.PhoneNumber,
            c.CreatedAt,
            c.UpdatedAt,
            c.Tasks?.Select(MapToTaskDto)
        );

    private static TaskItemDto MapToTaskDto(TaskItem t) =>
        new(
            t.Id,
            t.Title,
            t.Description,
            t.DueDate,
            t.IsCompleted,
            t.ContactId,
            null,              // Contact
            t.CreatedAt,
            t.UpdatedAt
        );

}
