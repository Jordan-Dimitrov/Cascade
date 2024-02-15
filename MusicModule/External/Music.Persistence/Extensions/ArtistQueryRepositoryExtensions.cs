using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Persistence.Shared;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.ValueObjects;
namespace Music.Persistence.Extensions
{
    internal static class ArtistQueryRepositoryExtensions
    {
        internal static IQueryable<Artist> FilterArtists(this IQueryable<Artist> artsts,
            int minFollowCount, int maxFollowCount) => artsts
            .Where(x => x.FollowCount.Value >= minFollowCount && x.FollowCount.Value <= minFollowCount);

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
