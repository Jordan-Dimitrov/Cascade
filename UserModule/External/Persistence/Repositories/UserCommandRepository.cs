using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.DomainEntities;

namespace Persistence.Repositories
{
    internal sealed class UserCommandRepository : IUserCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public UserCommandRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) 
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

        public async Task UpdateRefreshTokenAsync(User value, RefreshToken refreshToken)
        {
            RefreshToken oldRefreshToken = value.RefreshToken;
            value.SetRfreshToken(refreshToken);
            await Task.Run(() => _Context.Users.Update(value));
            await Task.Run(() => _Context.Remove(oldRefreshToken));
        }
    }
}
