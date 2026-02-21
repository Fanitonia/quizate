using System;
using Microsoft.EntityFrameworkCore;
using Quizate.Data.Models;

namespace Quizate.API.Data;

public class QuizateDbContext(DbContextOptions<QuizateDbContext> options) : DbContext(options)
{
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<QuizType> QuizTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                Username = "demo_user",
                Email = "demo_user@example.com",
                HashedPassword = "hashed_password_placeholder"
            }
        );

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz
            {
                Id = new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                Title = "Sample Quiz",
                Description = "This is a sample quiz.",
                IsPublic = true,
                QuizTypeId = new Guid("12345678-1234-1234-1234-123456789012"),
                CreatorId = new Guid("655a37fa-b9e1-4cad-a684-383ac587e906")
            }
        );

        modelBuilder.Entity<QuizType>().HasData(
            new QuizType
            {
                Id = new Guid("12345678-1234-1234-1234-123456789012"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654),
                Name = "standard",
                DisplayName = "Standard"
            }
        );
    }
}
