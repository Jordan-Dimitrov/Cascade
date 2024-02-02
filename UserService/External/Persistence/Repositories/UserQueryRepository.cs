using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.RequestFeatures;
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

        public async Task<ICollection<User>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Users;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }


        public async Task<User?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Users.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
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

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            if (refreshToken is null)
            {
                return null;
            }

            User? user = await _Context.Users.Where(x => x.RefreshToken.Token == new Token(refreshToken)).FirstOrDefaultAsync();

            if (user is not null)
            {
                await _Context.Entry(user).Reference(u => u.RefreshToken).LoadAsync();
            }

            return user;
        }

        public async Task<PagedList<User>> GetUsersWithPaginationAsync(UserParameters userParameters, bool trackChanges)
        {
            var query = _Context.Users.OrderBy(x => x.Username);

            var users = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
        }

    }
}
