using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> entity)
    {
        entity.Property(q => q.Title)
            .HasMaxLength(200);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_quizzes_title_not_empty", "char_length(trim(title)) > 0");
        });
    }
}
