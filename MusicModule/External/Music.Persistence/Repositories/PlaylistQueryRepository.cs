using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.PlaylistAggregate;
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
    internal sealed class PlaylistQueryRepository : IPlaylistQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public PlaylistQueryRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Playlist, bool>> condition)
        {
            return await _Context.Playlists.AnyAsync(condition);
        }

        public async Task<ICollection<Playlist>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Playlists;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<Playlist?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Playlists.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }


        public async Task<Playlist?> GetByNameAsync(string username)
        {
            PlaylistName name = new PlaylistName(username);

            Playlist? playlist = await _Context.Playlists.FirstOrDefaultAsync(x => x.PlaylistName == name);

            return playlist;
        }

        public async Task<PagedList<Playlist>> GetListenersWithPaginationAsync(PlaylistParameters playlistParameters, bool trackChanges)
        {
            var query = _Context.Playlists
                .FilterPlaylists(playlistParameters.MinDateCreated, playlistParameters.MaxDateCreated)
                .Search(playlistParameters.SearchTerm)
                .Sort(playlistParameters.OrderBy);

            var playlists = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Playlist>.ToPagedList(playlists, playlistParameters.PageNumber, playlistParameters.PageSize);
        }
    }
}
