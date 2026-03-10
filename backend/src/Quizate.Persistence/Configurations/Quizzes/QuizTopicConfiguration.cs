using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Core.Entities.Quizzes;

namespace Quizate.Persistence.Configurations.Quizzes;

internal class QuizTopicConfiguration : IEntityTypeConfiguration<QuizTopic>
{
    public void Configure(EntityTypeBuilder<QuizTopic> entity)
    {
        entity.HasKey(t => t.Name);

        entity.Property(t => t.Name)
            .HasMaxLength(25);

        entity.Property(t => t.DisplayName)
            .HasMaxLength(25);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_topics_name_not_empty", "char_length(trim(name)) > 0");
            t.HasCheckConstraint("ck_topics_display_name_not_empty", "char_length(trim(display_name)) > 0");
        });
    }
}
