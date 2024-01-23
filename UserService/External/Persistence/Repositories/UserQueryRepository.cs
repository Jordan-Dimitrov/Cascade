using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class UserQueryRepository : IUserQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public UserQueryRepository(ApplicationDbContext context) 
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> condition)
        {
            return await _Context.Users.AnyAsync(condition);
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _Context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByNameAsync(string username)
        {
            User? user = await _Context.Users.FromSql
                ($"SELECT * FROM Users WHERE Username = {username}")
                .FirstOrDefaultAsync();

            if(user is not null)
            {
                await _Context.Entry(user).Reference(u => u.RefreshToken).LoadAsync();
            }

            return user;
        }
    }
}
