using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Questions;

namespace Quizate.Persistence.Configurations.Questions;

internal class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
{
    public void Configure(EntityTypeBuilder<QuestionType> entity)
    {
        entity.HasKey(qt => qt.Name);

        entity.Property(qt => qt.Name)
            .HasMaxLength(20);

        entity.HasMany(qt => qt.Questions)
            .WithOne(q => q.QuestionType)
            .HasForeignKey(q => q.QuestionTypeName)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_quiz_types_name_not_empty", "char_length(trim(name)) > 0");
        });
    }
}
