using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Persistence.Repositories
{
    internal sealed class UserCommandRepository : IUserCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public UserCommandRepository(ApplicationDbContext context) 
        {
            _Context = context;
        }

        public async Task DeleteAsync(User value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(User value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(User value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
