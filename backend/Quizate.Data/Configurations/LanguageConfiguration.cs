using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> entity)
    {
        entity.HasKey(l => l.Code);

        entity.HasMany<Quiz>()
            .WithOne(q => q.Language)
            .HasForeignKey(q => q.LanguageCode)
            .HasPrincipalKey(l => l.Code)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Property(l => l.Code)
            .HasMaxLength(10);

        entity.HasIndex(l => l.Code)
            .IsUnique();

        entity.Property(l => l.Name)
            .HasMaxLength(20);
    }
}
