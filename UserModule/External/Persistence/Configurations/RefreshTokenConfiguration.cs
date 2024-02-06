using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.DomainEntities;
using Users.Domain.ValueObjects;

namespace Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.RefreshTokens), Schemas.UserSchema);

            builder.Property(x => x.Token).HasConversion(
            x => x.Value,
            value => new Token(value)).HasMaxLength(64);

            builder.OwnsOne(x => x.TokenDates, tokenDates =>
            {
                tokenDates.Property(td => td.TokenCreated).IsRequired();
                tokenDates.Property(td => td.TokenExpires).IsRequired();
            });

        }
    }
}
