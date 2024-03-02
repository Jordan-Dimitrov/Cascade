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
using Music.Domain.DomainEntities;

namespace Music.Persistence.Configurations
{
    internal class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable(nameof(Song) + "s", Schemas.MusicSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.SongName).HasConversion(
                songName => songName.Value,
                value => new SongName(value)).HasMaxLength(25);

            builder.Property(x => x.AudioFile).HasConversion(
                audioFile => audioFile.Value,
                value => new AudioFile(value));

            builder.Property(x => x.SongCategory).HasConversion(
                category => category.Value,
                value => new SongCategory(value)).HasMaxLength(16);

        }
    }
}
