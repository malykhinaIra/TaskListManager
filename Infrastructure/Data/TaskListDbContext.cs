using Microsoft.EntityFrameworkCore;
using TestTask1.Domain.Entities;

namespace TestTask1.Infrastructure.Data;

public class TaskListDbContext : DbContext
{
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options)
    {
    }

    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<TaskListUser> TaskListUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.OwnerId).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasIndex(e => e.OwnerId);
            entity.HasIndex(e => e.CreatedAt);
        });

        modelBuilder.Entity<TaskListUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TaskListId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            
            entity.HasOne(e => e.TaskList)
                .WithMany(e => e.TaskListUsers)
                .HasForeignKey(e => e.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.TaskListId, e.UserId }).IsUnique();
        });
    }
} 