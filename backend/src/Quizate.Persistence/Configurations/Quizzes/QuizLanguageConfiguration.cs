using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Persistence.Configurations.Quizzes;

internal class QuizLanguageConfiguration : IEntityTypeConfiguration<QuizLanguage>
{
    public void Configure(EntityTypeBuilder<QuizLanguage> entity)
    {
        entity.HasKey(l => l.Code);

        entity.HasMany(l => l.Quizzes)
            .WithOne(q => q.Language)
            .HasForeignKey(q => q.LanguageCode)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Property(l => l.Code)
            .HasMaxLength(10);
    }
}
