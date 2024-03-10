using Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.ValueObjects;

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
        }
    }
}
