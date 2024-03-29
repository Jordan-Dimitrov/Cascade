﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Shared.Outbox
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Error);
            builder.Property(x => x.ProcessedOnUtc);
            builder.Property(x => x.Content);
            builder.Property(x => x.OccuredOnUtc);
            builder.Property(x => x.Type);
        }
    }
}
