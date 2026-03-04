using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class MultipleChoiceQuestionConfiguration : IEntityTypeConfiguration<MultipleChoiceQuestion>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> entity)
    {
        entity.Property(e => e.Text)
            .HasMaxLength(1000);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_multiple_choice_questions_text_not_empty", "char_length(trim(text)) > 0");
        });
    }
}
