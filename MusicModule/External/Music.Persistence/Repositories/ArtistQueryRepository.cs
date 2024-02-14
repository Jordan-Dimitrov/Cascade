using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.RequestFeatures;
using Music.Domain.ValueObjects;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            var query = _Context.Artists.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }


        public async Task<Artist?> GetUserByNameAsync(string username)
        {
            Username name = new Username(username);

            Artist? user = await _Context.Artists.FirstOrDefaultAsync(x => x.Username == name);

            return user;
        }

        public async Task<PagedList<Artist>> GetUsersWithPaginationAsync(ArtistParameters artistParameters, bool trackChanges)
        {
            var query = _Context.Artists
                .FilterArtists(artistParameters.MinFollowCount, artistParameters.MaxFollowCount)
                .Search(artistParameters.SearchTerm)
                .Sort(artistParameters.OrderBy);

            var users = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Artist>.ToPagedList(users, artistParameters.PageNumber, artistParameters.PageSize);
        }
    }
}
