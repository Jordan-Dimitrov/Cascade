using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;

namespace Music.Persistence.Repositories
{
    internal sealed class AlbumCommandRepository : IAlbumCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public AlbumCommandRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task DeleteAsync(Album value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(Album value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(Album value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
