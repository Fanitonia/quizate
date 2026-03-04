using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> entity)
    {
        entity.Property(t => t.Name)
            .HasMaxLength(25);

        entity.HasIndex(t => t.Name)
            .IsUnique();

        entity.Property(t => t.DisplayName)
            .HasMaxLength(25);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_topics_name_not_empty", "char_length(trim(name)) > 0");
            t.HasCheckConstraint("ck_topics_display_name_not_empty", "char_length(trim(display_name)) > 0");
        });
    }
}
