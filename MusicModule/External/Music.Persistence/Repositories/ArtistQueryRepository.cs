﻿using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.RequestFeatures;
using Music.Domain.ValueObjects;
using Music.Persistence.Extensions;
using System.Linq.Expressions;

namespace Music.Persistence.Repositories
{
    internal sealed class ArtistQueryRepository : IArtistQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public ArtistQueryRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Artist, bool>> condition)
        {
            return await _Context.Artists.AnyAsync(condition);
        }

        public async Task<ICollection<Artist>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Artists;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }


        public async Task<Artist?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Artists.Where(x => x.Id == id)
                .Include(x => x.Albums)
                    .ThenInclude(c => c.Songs);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<Artist?> GetByNameAsync(string username)
        {
            Username name = new Username(username);

            Artist? artist = await _Context.Artists.FirstOrDefaultAsync(x => x.Username == name);

            return artist;
        }

        public async Task<PagedList<Artist>> GetArtistsWithPaginationAsync(ArtistParameters artistParameters, bool trackChanges)
        {
            var query = _Context.Artists
                .FilterArtists(artistParameters.MinAlbumCount, artistParameters.MaxAlbumCount)
                .Search(artistParameters.SearchTerm)
                .Sort(artistParameters.OrderBy);

            var artists = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Artist>.ToPagedList(artists, artistParameters.PageNumber, artistParameters.PageSize);
        }
    }
}
