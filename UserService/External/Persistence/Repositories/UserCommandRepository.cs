using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
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

        public async Task<bool> DeleteAsync(User value)
        {
            await Task.Run( () => _Context.Remove(value));

            return await _UnitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertAsync(User value)
        {
            await _Context.AddAsync(value);

            return await _UnitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User value)
        {
            await Task.Run( () => _Context.Update(value));

            return await _UnitOfWork.SaveChangesAsync() > 0;
        }
    }
}
