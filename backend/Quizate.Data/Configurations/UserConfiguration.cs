using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.Property(u => u.Username)
            .HasMaxLength(25);

        entity.Property(u => u.NormalizedUsername)
            .HasMaxLength(25)
            .HasComputedColumnSql("lower(username)", stored: true);

        entity.HasIndex(u => u.NormalizedUsername)
            .IsUnique();

        entity.HasIndex(u => u.Username)
            .IsUnique();

        entity.Property(u => u.Email)
            .HasMaxLength(255);

        entity.HasIndex(u => u.Email)
            .IsUnique();

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_users_username_not_empty", "char_length(trim(username)) > 0");
            t.HasCheckConstraint("ck_users_password_hash_not_empty", "char_length(trim(password_hash)) > 0");
            t.HasCheckConstraint("ck_users_username_format", "username ~ '^[A-Za-z0-9_]+$'");
        });
    }
}
