using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class RefreshTokenqQueryRepository : IRefreshTokenQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public RefreshTokenqQueryRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<RefreshToken, bool>> condition)
        {
            return await _Context.RefreshTokens.AnyAsync(condition);
        }

        public async Task<ICollection<RefreshToken>> GetAllAsync()
        {
            return await _Context.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken?> GetByIdAsync(Guid id)
        {
            return await _Context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
