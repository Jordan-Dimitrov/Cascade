using Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.ValueObjects;

namespace Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.Users), Schemas.UserSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.Username).HasConversion(
                username => username.Value,
                value => new Username(value)).HasMaxLength(50);
        }
    }
}
