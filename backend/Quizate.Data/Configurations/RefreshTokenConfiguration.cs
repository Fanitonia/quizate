using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Data.Models;

namespace Quizate.Data.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
        entity.HasIndex(rt => rt.TokenHash)
            .IsUnique();

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_refresh_tokens_token_hash_not_empty", "char_length(trim(token_hash)) > 0");
        });
    }
}
