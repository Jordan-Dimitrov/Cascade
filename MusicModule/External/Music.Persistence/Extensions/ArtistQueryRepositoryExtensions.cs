﻿using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.ValueObjects;
using Persistence.Shared;
using System.Linq.Dynamic.Core;
namespace Music.Persistence.Extensions
{
    internal static class ArtistQueryRepositoryExtensions
    {
        internal static IQueryable<Artist> FilterArtists(this IQueryable<Artist> artsts,
            int minAlbumCount, int maxAlbumCount) => artsts
            .Where(x => x.Albums.Count >= minAlbumCount && x.Albums.Count <= maxAlbumCount);

        internal static IQueryable<Artist> Search(this IQueryable<Artist> artsts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return artsts;
            }

            Username username = new Username(searchTerm);

            return artsts.Where(x => x.Username == username);
        }

        internal static IQueryable<Artist> Sort(this IQueryable<Artist> artsts,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return artsts.OrderBy(e => e.Username);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Artist>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return artsts.OrderBy(e => e.Username);
            }

            return artsts.OrderBy(orderQuery);
        }
    }
}
