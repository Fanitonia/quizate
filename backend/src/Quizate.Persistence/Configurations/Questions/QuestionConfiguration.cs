using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Questions;

namespace Quizate.Persistence.Configurations.Questions;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> entity)
    {
        entity.Property(q => q.Payload)
            .IsRequired()
            .HasColumnType("jsonb");
    }
}
