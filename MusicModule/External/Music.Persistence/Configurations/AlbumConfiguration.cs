using Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.ValueObjects;

namespace Music.Persistence.Configurations
{
    internal class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable(nameof(Album), Schemas.MusicSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.AlbumName).HasConversion(
                albumName => albumName.Value,
                value => new AlbumName(value)).HasMaxLength(16);
        }
    }
}
