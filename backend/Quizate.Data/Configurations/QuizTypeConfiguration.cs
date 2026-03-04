using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class QuizTypeConfiguration : IEntityTypeConfiguration<QuizType>
{
    public void Configure(EntityTypeBuilder<QuizType> entity)
    {
        entity.Property(qt => qt.Name)
            .HasMaxLength(20);

        entity.HasIndex(qt => qt.Name)
            .IsUnique();

        entity.Property(qt => qt.DisplayName)
            .HasMaxLength(25);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_quiz_types_name_not_empty", "char_length(trim(name)) > 0");
            t.HasCheckConstraint("ck_quiz_types_display_name_not_empty", "char_length(trim(display_name)) > 0");
        });
    }
}
