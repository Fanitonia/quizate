using Microsoft.EntityFrameworkCore;
using Quizate.Data.Models;

namespace Quizate.API.Data;

public class QuizateDbContext(DbContextOptions<QuizateDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<QuizTopic> QuizTopics { get; set; }
    public DbSet<QuizLanguage> QuizLanguages { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }

    private readonly string MutlipleChoiceQuestionPayload = @"{""Title"":""What's the capital of Turkey?"",""Options"":[{""Text"":""Istanbul"",""IsCorrect"":false},{""Text"":""Ankara"",""IsCorrect"":true},{""Text"":""Izmir"",""IsCorrect"":false},{""Text"":""Bursa"",""IsCorrect"":false}]}";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizateDbContext).Assembly);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                Username = "demo_user",
                Email = "demo_user@example.com",
                PasswordHash = "hashed_password_placeholder"
            }
        );

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz
            {
                Id = new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                Title = "Sample Quiz",
                Description = "This is a sample quiz.",
                IsPublic = true,
                CreatorId = new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                LanguageCode = "en",
            }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question
            {
                Id = 1,
                QuizId = new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                QuestionTypeName = "multiple_choice",
                Payload = MutlipleChoiceQuestionPayload
            }
        );

        modelBuilder.Entity<QuestionType>().HasData(
            new QuestionType
            {
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                Name = "multiple_choice",
            }
        );

        modelBuilder.Entity<QuizTopic>().HasData(
            new QuizTopic
            {
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                Name = "geography",
                DisplayName = "Geography",
            }
        );

        modelBuilder.Entity<QuizAttempt>().HasData(
            new QuizAttempt
            {
                Id = 1,
                QuizId = new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                UserId = new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                CreatedAt = new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                Score = 50,
            }
        );

        modelBuilder.Entity<QuizLanguage>().HasData(
            new QuizLanguage
            {
                Code = "en",
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
