using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.ValueObjects;
using Domain.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Configurations
{
    internal class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.Artists), Schemas.MusicSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.Username).HasConversion(
                username => username.Value,
                value => new Username(value)).HasMaxLength(50);

            builder.Property(x => x.FollowCount).HasConversion(
                count => count.Value,
                value => new FollowCount(value));

        }
    }
}
