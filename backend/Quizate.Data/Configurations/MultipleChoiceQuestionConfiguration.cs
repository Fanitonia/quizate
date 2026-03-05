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

        entity.Property(e => e.ImageUrl)
            .HasMaxLength(1024);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_multiple_choice_questions_text_not_empty", "char_length(trim(text)) > 0");
            t.HasCheckConstraint("ck_multiple_choice_questions_display_order_non_negative", "display_order >= 0");
        });
    }
}
