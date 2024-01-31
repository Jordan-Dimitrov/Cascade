﻿using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class RefreshTokenCommandRepository : IRefreshTokenCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IUnitOfWork _UnitOfWork;
        public RefreshTokenCommandRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _Context = context;
            _UnitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(RefreshToken value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(RefreshToken value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(RefreshToken value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
