using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Users;

namespace Quizate.Persistence.EntityConfigurations.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.Property(u => u.Username)
            .HasMaxLength(25)
            .IsRequired();

        entity.HasIndex(u => u.Username)
            .IsUnique();

        entity.Property(u => u.NormalizedUsername)
            .HasMaxLength(25)
            .HasComputedColumnSql("lower(username)", stored: true);

        entity.HasIndex(u => u.NormalizedUsername)
            .IsUnique();

        entity.Property(u => u.Email)
            .HasMaxLength(255);

        entity.HasIndex(u => u.Email)
            .IsUnique();

        entity.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(1024);

        entity.Property(u => u.PasswordHash)
            .IsRequired();

        entity.Property(u => u.Role)
            .IsRequired();

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_users_username_not_empty", "char_length(trim(username)) > 0");
            t.HasCheckConstraint("ck_users_password_hash_not_empty", "char_length(trim(password_hash)) > 0");
            t.HasCheckConstraint("ck_users_username_format", "username ~ '^[A-Za-z0-9_]+$'");
        });
    }
}
