using Music.Domain.Aggregates.ListenerAggregate;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shared.RequestFeatures;
using Music.Domain.RequestFeatures;

namespace Music.Domain.Abstractions
{
    public interface IListenerQueryRepository : IQueryRepository<Listener>
    {
        Task<PagedList<Listener>> GetListenersWithPaginationAsync(ListenerParameters listenerParameters, bool trackChanges);
    }
}
