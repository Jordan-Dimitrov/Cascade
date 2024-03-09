using Microsoft.EntityFrameworkCore;
using Domain.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.RequestFeatures;
using Users.Domain.ValueObjects;

namespace Persistence.Repositories
{
    internal sealed class UserQueryRepository : IUserQueryRepository
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


        public async Task<User?> GetByNameAsync(string username)
        {
            Username name = new Username(username);

            User? user = await _Context.Users.FirstOrDefaultAsync(x => x.Username == name);

            return user;
        }

        public async Task<PagedList<User>> GetUsersWithPaginationAsync(UserParameters userParameters, bool trackChanges)
        {
            var query = _Context.Users
                .FilterUsers(userParameters.MinRole, userParameters.MaxRole)
                .Search(userParameters.SearchTerm)
                .Sort(userParameters.OrderBy);

            var users = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
        }
    }
}
