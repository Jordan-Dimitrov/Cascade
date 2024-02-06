using Users.Application.Dtos;
using MediatR;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.RequestFeatures;

namespace Users.Application.Users.Queries
{
    public sealed record GetUsersQuery(UserParameters RequestParameters) : IRequest<(IEnumerable<ExpandoObject> users, MetaData metaData)>;
}
