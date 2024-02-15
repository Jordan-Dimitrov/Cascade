using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
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
    internal sealed class AlbumQueryRepository : IAlbumQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public AlbumQueryRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Album, bool>> condition)
        {
            return await _Context.Albums.AnyAsync(condition);
        }

        public async Task<ICollection<Album>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Albums;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<Album?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Albums.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<Album?> GetByNameAsync(string name)
        {
            AlbumName albumName = new AlbumName(name);

            Album? album = await _Context.Albums.FirstOrDefaultAsync(x => x.AlbumName == albumName);

            return album;
        }

        public async Task<PagedList<Album>> GetAlbumsWithPaginationAsync(AlbumParameters albumParameters, bool trackChanges)
        {
            var query = _Context.Albums
                .FilterAlbums(albumParameters.MinDateCreated, albumParameters.MaxDateCreated)
                .Search(albumParameters.SearchTerm)
                .Sort(albumParameters.OrderBy);

            var albums = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Album>.ToPagedList(albums, albumParameters.PageNumber, albumParameters.PageSize);
        }
    }
}
