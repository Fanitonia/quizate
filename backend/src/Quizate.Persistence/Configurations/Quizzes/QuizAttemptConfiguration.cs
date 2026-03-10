using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Core.Entities.Quizzes;

namespace Quizate.Persistence.Configurations.Quizzes;

internal class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
{
    public void Configure(EntityTypeBuilder<QuizAttempt> entity)
    {
        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_quiz_attempts_score_non_negative", "score >= 0");
        });
    }
}
