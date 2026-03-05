using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class MultipleChoiceOptionConfiguration : IEntityTypeConfiguration<MultipleChoiceOption>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceOption> entity)
    {
        entity.Property(e => e.Text)
            .HasMaxLength(500);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_multiple_choice_options_text_not_empty", "char_length(trim(text)) > 0");
            t.HasCheckConstraint("ck_multiple_choice_options_display_order_non_negative", "display_order >= 0");
        });
    }
}
