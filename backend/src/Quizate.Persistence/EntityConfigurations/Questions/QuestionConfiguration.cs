using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Questions;

namespace Quizate.Persistence.EntityConfigurations.Questions;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> entity)
    {
        entity.HasKey(q => q.Id);

        entity.Property(q => q.QuestionTypeName)
            .IsRequired();

        entity.Property(q => q.Payload)
            .IsRequired()
            .HasColumnType("jsonb");
    }
}
