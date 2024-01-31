using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class UserCommandRepository : IUserCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IUnitOfWork _UnitOfWork;
        public UserCommandRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) 
        {
            _Context = context;
            _UnitOfWork = unitOfWork;
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
        public async Task UpdateRefreshTokenAsync(User value)
        {
            await Task.Run(() => _Context.Users
                .FromSql($"UPDATE Users SET RefreshTokenId = {value.RefreshTokenId} WHERE Id = {value.Id};"));
        }
    }
}
