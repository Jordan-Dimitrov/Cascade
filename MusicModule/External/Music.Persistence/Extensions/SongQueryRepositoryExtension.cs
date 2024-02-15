using Music.Domain.Aggregates.PlaylistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.ValueObjects;
using Persistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Music.Persistence.Extensions
{
    internal static class SongQueryRepositoryExtension
    {
        internal static IQueryable<Song> FilterSongs(this IQueryable<Song> songs,
            DateTime minDateCreated, DateTime maxDateCreated) => songs
            .Where(x => x.DateCreated >= minDateCreated && x.DateCreated <= maxDateCreated);

        internal static IQueryable<Song> Search(this IQueryable<Song> songs, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return songs;
            }

            SongName songName = new SongName(searchTerm);

            return songs.Where(x => x.SongName == songName);
        }

        internal static IQueryable<Song> Sort(this IQueryable<Song> songs,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return songs.OrderBy(e => e.SongName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Song>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return songs.OrderBy(e => e.SongName);
            }

            return songs.OrderBy(orderQuery);
        }
    }
}
