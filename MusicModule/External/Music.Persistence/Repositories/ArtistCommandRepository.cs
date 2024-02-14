﻿using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Repositories
{
    internal sealed class ArtistCommandRepository : IArtistCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public ArtistCommandRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task DeleteAsync(Artist value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(Artist value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(Artist value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
