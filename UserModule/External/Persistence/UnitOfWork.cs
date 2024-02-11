using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
