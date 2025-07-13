using Microsoft.EntityFrameworkCore;
using TestTask1.Infrastructure.Data.Models;

namespace TestTask1.Infrastructure.Data;

public class TaskListDbContext : DbContext
{
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options)
    {
    }

    public DbSet<TaskListModel> TaskLists { get; set; }
    public DbSet<TaskListUserModel> TaskListUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskListModel>()
            .HasMany(model => model.TaskListUsers)
            .WithOne(model => model.TaskList)
            .HasForeignKey(model => model.TaskListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 