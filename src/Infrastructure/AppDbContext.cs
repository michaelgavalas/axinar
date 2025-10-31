using Axinar.Domain.Contacts;
using Axinar.Domain.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Axinar.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Using the public schema for safety, won't be playing with schemas in this project
        modelBuilder.HasDefaultSchema("public");

        // Build "contacts" table
        modelBuilder.Entity<Contact>(b =>
        {
            b.ToTable("contacts");
            b.HasKey(x => x.Id);

            b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            b.Property(x => x.Email).IsRequired().HasMaxLength(256);
            b.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);

            b.Property(x => x.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            b.Property(x => x.UpdatedAt)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            b.HasIndex(x => x.Email).IsUnique();

            // Relationship
            b.HasMany(x => x.Tasks)
             .WithOne(t => t.Contact)
             .HasForeignKey(t => t.ContactId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Build "task_items" table
        modelBuilder.Entity<TaskItem>(b =>
        {
            b.ToTable("task_items");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(2000);

            b.Property(x => x.IsCompleted)
                .HasDefaultValue(false);

            b.Property(x => x.DueDate)
                .HasColumnType("timestamp with time zone");

            b.Property(x => x.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            b.Property(x => x.UpdatedAt)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");
        });
    }

    // Function to automatically update UpdatedAt field when modifying entities
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                if (entry.Metadata.FindProperty("UpdatedAt") != null)
                    entry.CurrentValues["UpdatedAt"] = utcNow;
            }

            if (entry.State == EntityState.Added)
            {
                if (entry.Metadata.FindProperty("CreatedAt") != null && entry.CurrentValues["CreatedAt"] == null)
                    entry.CurrentValues["CreatedAt"] = utcNow;

                if (entry.Metadata.FindProperty("UpdatedAt") != null && entry.CurrentValues["UpdatedAt"] == null)
                    entry.CurrentValues["UpdatedAt"] = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
