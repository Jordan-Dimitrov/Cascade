using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ListenerAggregate;
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
    internal sealed class ListenerQueryRepository : IListenerQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        public ListenerQueryRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Listener, bool>> condition)
        {
            return await _Context.Listeners.AnyAsync(condition);
        }

        public async Task<ICollection<Listener>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Listeners;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }


        public async Task<Listener?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Listeners.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }


        public async Task<Listener?> GetByNameAsync(string username)
        {
            Username name = new Username(username);

            Listener? listener = await _Context.Listeners.FirstOrDefaultAsync(x => x.Username == name);

            return listener;
        }

        public async Task<PagedList<Listener>> GetListenersWithPaginationAsync(ListenerParameters listenerParameters, bool trackChanges)
        {
            var query = _Context.Listeners
                .FilterListeners(listenerParameters.MinPlaylistCount, listenerParameters.MaxPlaylistCount)
                .Search(listenerParameters.SearchTerm)
                .Sort(listenerParameters.OrderBy);

            var listeners = await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());

            return PagedList<Listener>.ToPagedList(listeners, listenerParameters.PageNumber, listenerParameters.PageSize);
        }
    }
}
