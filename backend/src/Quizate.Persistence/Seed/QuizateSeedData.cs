using Microsoft.EntityFrameworkCore;
using Quizate.Domain.Entities.Questions;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Domain.Entities.Users;

namespace Quizate.Persistence.Seed;

internal static class QuizateSeedData
{
    private static readonly string MultipleChoiceQuestionPayload = @"{""Title"":""What's the capital of Turkey?"",""Options"":[{""Text"":""Istanbul"",""IsCorrect"":false},{""Text"":""Ankara"",""IsCorrect"":true},{""Text"":""Izmir"",""IsCorrect"":false},{""Text"":""Bursa"",""IsCorrect"":false}]}";

    public static ModelBuilder SeedInitialData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User(
                "demo_user",
                "hashed_password_placeholder",
                "demo_user@example.com",
                id: new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                createdAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                updatedAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc))
        );

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz(
                "Sample Quiz",
                "en",
                creatorId: new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                description: "This is a sample quiz.",
                isPublic: true,
                id: new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                createdAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                updatedAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc))
        );

        modelBuilder.Entity<Question>().HasData(
            new Question(
                new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                "multiple_choice",
                MultipleChoiceQuestionPayload,
                id: 1)
        );

        modelBuilder.Entity<QuestionType>().HasData(
            new QuestionType(
                "multiple_choice",
                createdAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                updatedAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc))
        );

        modelBuilder.Entity<QuizTopic>().HasData(
            new QuizTopic(
                "geography",
                "Geography",
                createdAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc),
                updatedAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc))
        );

        modelBuilder.Entity<QuizAttempt>().HasData(
            new QuizAttempt(
                new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                50,
                id: 1,
                userId: new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                createdAt: new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc))
        );

        modelBuilder.Entity<QuizLanguage>().HasData(
            new QuizLanguage("en")
        );

        return modelBuilder;
    }
}
