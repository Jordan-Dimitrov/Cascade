using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;

namespace Persistence
{
    internal sealed class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        public UserUnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
