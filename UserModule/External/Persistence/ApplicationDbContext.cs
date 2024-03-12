using Microsoft.EntityFrameworkCore;
using Persistence.Shared.Outbox;
using Users.Domain.Aggregates.UserAggregate;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxMessageConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
