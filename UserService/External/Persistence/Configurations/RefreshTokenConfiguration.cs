using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(x => x.Token).HasConversion(
            x => x.Value,
            value => new Token(value));

            builder.OwnsOne(x => x.TokenDates, tokenDates =>
            {
                tokenDates.Property(td => td.TokenCreated).IsRequired();
                tokenDates.Property(td => td.TokenExpires).IsRequired();
            });

        }
    }
}
