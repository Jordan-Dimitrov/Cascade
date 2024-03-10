using Domain.Shared.RequestFeatures;
using MediatR;
using System.Dynamic;
using Users.Domain.RequestFeatures;

namespace Users.Application.Users.Queries
{
    public sealed record GetUsersQuery(UserParameters RequestParameters) : IRequest<(IEnumerable<ExpandoObject> users, MetaData metaData)>;
}
