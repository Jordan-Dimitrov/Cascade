using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
           // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Cascade;Integrated Security=True;");
        //}
    }
}
