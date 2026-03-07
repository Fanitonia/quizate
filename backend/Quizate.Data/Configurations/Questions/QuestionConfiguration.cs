using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> entity)
    {
        entity.Property(q => q.Payload)
            .IsRequired()
            .HasColumnType("jsonb");
    }
}
