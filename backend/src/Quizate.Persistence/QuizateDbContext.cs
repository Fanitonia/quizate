using Microsoft.EntityFrameworkCore;
using Quizate.Core.Entities.Questions;
using Quizate.Core.Entities.Quizzes;
using Quizate.Core.Entities.Users;
using Quizate.Persistence.Seed;

namespace Quizate.Persistence;

public class QuizateDbContext(DbContextOptions<QuizateDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<QuizTopic> QuizTopics { get; set; }
    public DbSet<QuizLanguage> QuizLanguages { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizateDbContext).Assembly);

        modelBuilder.SeedInitialData();

        base.OnModelCreating(modelBuilder);
    }
}
