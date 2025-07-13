using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace TestTask1.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TaskListDbContext))]
    partial class TaskListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TestTask1.Infrastructure.Data.Models.TaskListModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("OwnerId");

                    b.HasIndex("OwnerId", "CreatedAt");

                    b.ToTable("TaskLists");
                });

            modelBuilder.Entity("TestTask1.Infrastructure.Data.Models.TaskListUserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TaskListId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskListId", "UserId")
                        .IsUnique();

                    b.HasIndex("TaskListId");

                    b.HasIndex("UserId");

                    b.HasIndex("CreatedAt");

                    b.ToTable("TaskListUsers");
                });

            modelBuilder.Entity("TestTask1.Infrastructure.Data.Models.TaskListUserModel", b =>
                {
                    b.HasOne("TestTask1.Infrastructure.Data.Models.TaskListModel", "TaskList")
                        .WithMany("TaskListUsers")
                        .HasForeignKey("TaskListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("TestTask1.Infrastructure.Data.Models.TaskListModel", b =>
                {
                    b.Navigation("TaskListUsers");
                });
        }
    }
} 