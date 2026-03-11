using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizate.Domain.Entities.Users;

namespace Quizate.Persistence.EntityConfigurations.Users;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
        entity.HasKey(rt => rt.TokenHash);

        entity.ToTable(t =>
        {
            t.HasCheckConstraint("ck_refresh_tokens_token_hash_not_empty", "char_length(trim(token_hash)) > 0");
        });
    }
}
