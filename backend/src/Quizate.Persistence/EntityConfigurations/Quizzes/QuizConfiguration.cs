using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Persistence.EntityConfigurations.Quizzes;

internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> entity)
    {
        entity.Property(q => q.Title)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(q => q.Description)
            .HasMaxLength(400);

        entity.Property(q => q.ThumbnailUrl)
            .HasMaxLength(1024);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_quizzes_title_not_empty", "char_length(trim(title)) > 0");
        });
    }
}
