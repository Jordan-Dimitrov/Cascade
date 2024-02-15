using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.ListenerAggregate;
using Music.Domain.ValueObjects;
using Persistence.Shared;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Extensions
{
    internal static class ListenerQueryRepositoryExtension
    {
        internal static IQueryable<Listener> FilterListeners(this IQueryable<Listener> listeners,
            int minPlaylistCount, int maxPlaylistCount) => listeners
            .Where(x => x.Playlists.Count >= minPlaylistCount && x.Playlists.Count <= maxPlaylistCount);

        internal static IQueryable<Listener> Search(this IQueryable<Listener> listeners, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return listeners;
            }

            Username username = new Username(searchTerm);

            return listeners.Where(x => x.Username == username);
        }

        internal static IQueryable<Listener> Sort(this IQueryable<Listener> listeners,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return listeners.OrderBy(e => e.Username);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Listener>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return listeners.OrderBy(e => e.Username);
            }

            return listeners.OrderBy(orderQuery);
        }
    }
}
