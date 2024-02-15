using Music.Domain.Abstractions;
using Music.Domain.Aggregates.PlaylistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Repositories
{
    internal sealed class PlaylistCommandRepository : IPlaylistCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public PlaylistCommandRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task DeleteAsync(Playlist value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(Playlist value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(Playlist value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
