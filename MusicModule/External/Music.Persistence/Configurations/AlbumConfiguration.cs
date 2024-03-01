using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Aggregates.ArtistAggregate;
using Domain.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Domain.ValueObjects;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.DomainEntities;

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

            builder.HasMany<AlbumSong>()
               .WithOne()
               .HasForeignKey(song => song.AlbumId);
        }
    }
}
