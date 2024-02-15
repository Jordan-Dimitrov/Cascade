using Music.Domain.Abstractions;
using Music.Domain.Aggregates.SongAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Repositories
{
    internal sealed class SongCommandRepository : ISongCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public SongCommandRepository(ApplicationDbContext context) 
        {
            _Context = context;
        }
        public async Task DeleteAsync(Song value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(Song value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(Song value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
