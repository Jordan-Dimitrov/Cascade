using Music.Domain.Aggregates.PlaylistAggregate;
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
    internal static class PlaylistQueryRepositoryExtension
    {
        internal static IQueryable<Playlist> FilterPlaylists(this IQueryable<Playlist> playlists,
            DateTime minDateCreated, DateTime maxDateCreated) => playlists
            .Where(x => x.DateCreated >= minDateCreated && x.DateCreated <= maxDateCreated);

        internal static IQueryable<Playlist> Search(this IQueryable<Playlist> playlists, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return playlists;
            }

            PlaylistName playlistName = new PlaylistName(searchTerm);

            return playlists.Where(x => x.PlaylistName == playlistName);
        }

        internal static IQueryable<Playlist> Sort(this IQueryable<Playlist> playlists,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return playlists.OrderBy(e => e.PlaylistName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Playlist>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return playlists.OrderBy(e => e.PlaylistName);
            }

            return playlists.OrderBy(orderQuery);
        }
    }
}
