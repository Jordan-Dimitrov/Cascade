using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.ValueObjects;
using System.Linq.Dynamic.Core;
using Persistence.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Extensions
{
    internal static class AlbumQueryRepositoryExtension
    {
        internal static IQueryable<Album> FilterAlbums(this IQueryable<Album> albums,
            DateTime minDateCreated, DateTime maxDateCreated) => albums
            .Where(x => x.DateCreated >= minDateCreated && x.DateCreated <= maxDateCreated);

        internal static IQueryable<Album> Search(this IQueryable<Album> albums, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return albums;
            }

            AlbumName albumName = new AlbumName(searchTerm);

            return albums.Where(x => x.AlbumName == albumName);
        }

        internal static IQueryable<Album> Sort(this IQueryable<Album> albums,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return albums.OrderBy(e => e.AlbumName);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Album>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return albums.OrderBy(e => e.AlbumName);
            }

            return albums.OrderBy(orderQuery);
        }
    }
}
