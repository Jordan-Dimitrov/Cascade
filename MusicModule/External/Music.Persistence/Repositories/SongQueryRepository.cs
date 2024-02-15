using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.PlaylistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.RequestFeatures;
using Music.Domain.ValueObjects;
using Music.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Repositories
{
    internal sealed class SongQueryRepository : ISongQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public SongQueryRepository(ApplicationDbContext context) 
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Song, bool>> condition)
        {
            return await _Context.Songs.AnyAsync(condition);
        }

        public async Task<ICollection<Song>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Songs;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<Song?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Songs.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }


        public async Task<Song?> GetByNameAsync(string username)
        {
            SongName name = new SongName(username);

            Song? song = await _Context.Songs.FirstOrDefaultAsync(x => x.SongName == name);

            return song;
        }

        public async Task<PagedList<Song>> GetSongsWithPaginationAsync(SongParameters songParameters, bool trackChanges)
        {
            var query = _Context.Songs
                .FilterSongs(songParameters.MinDateCreated, songParameters.MaxDateCreated)
                .Search(songParameters.SearchTerm)
                .Sort(songParameters.OrderBy);

            var songs = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Song>.ToPagedList(songs, songParameters.PageNumber, songParameters.PageSize);
        }
    }
}
