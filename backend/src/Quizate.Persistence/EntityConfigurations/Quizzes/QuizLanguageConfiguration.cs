using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Persistence.EntityConfigurations.Quizzes;

internal class QuizLanguageConfiguration : IEntityTypeConfiguration<QuizLanguage>
{
    public void Configure(EntityTypeBuilder<QuizLanguage> entity)
    {
        entity.HasKey(l => l.Code);

        entity.Property(l => l.Code)
            .HasMaxLength(10);

        entity.HasMany(l => l.Quizzes)
            .WithOne(q => q.Language)
            .HasForeignKey(q => q.LanguageCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
