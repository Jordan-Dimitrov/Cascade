using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Domain.ValueObjects;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.Aggregates.PlaylistAggregate;

namespace Music.Persistence.Configurations
{
    internal class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable(nameof(Playlist), Schemas.MusicSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.PlaylistName).HasConversion(
                playlistName => playlistName.Value,
                value => new PlaylistName(value)).HasMaxLength(16);
        }
    }
}
