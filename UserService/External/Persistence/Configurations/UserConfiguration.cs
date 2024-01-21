using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Username).HasConversion(
                username => username.Value,
                value => new Username(value));
            
            builder.Property(x => x.PasswordHash).HasMaxLength(int.MaxValue);
            builder.Property(x => x.PasswordSalt).HasMaxLength(int.MaxValue);

            builder.Property(x => x.PermissionType)
            .HasConversion<int>()
            .IsRequired();
        }
    }
}
