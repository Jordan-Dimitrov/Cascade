using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Users.Domain.ValueObjects;
using Users.Domain.Aggregates.UserAggregate;
using Persistence.Shared;
using Domain.Shared.Constants;
namespace Persistence.Repositories
{
    internal static class UserQueryRepositoryExtensions
    {
        internal static IQueryable<User> FilterUsers(this IQueryable<User> users,
            UserRole minRole, UserRole maxRole) => users
            .Where(x => (x.PermissionType >= minRole && x.PermissionType <= maxRole));

        internal static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return users;
            }

            Username username = new Username(searchTerm);

            return users.Where(x => x.Username == username);
        }

        internal static IQueryable<User> Sort(this IQueryable<User> users,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return users.OrderBy(e => e.Username);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return users.OrderBy(e => e.Username);
            }

            return users.OrderBy(orderQuery);
        }
    }
}
