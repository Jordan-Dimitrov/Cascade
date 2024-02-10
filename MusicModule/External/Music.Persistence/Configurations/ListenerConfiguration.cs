using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Aggregates.ArtistAggregate;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Domain.Aggregates.ListenerAggregate;
using Music.Domain.ValueObjects;
using Music.Domain.Aggregates.SongAggregate;

namespace Music.Persistence.Configurations
{
    internal class ListenerConfiguration : IEntityTypeConfiguration<Listener>
    {
        public void Configure(EntityTypeBuilder<Listener> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.Listeners), Schemas.MusicSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.Username).HasConversion(
                username => username.Value,
                value => new Username(value)).HasMaxLength(50);
        }
    }
}
