using Application.Dtos;
using Domain.Abstractions;
using Domain.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public sealed record GetUsersQuery(UserParameters RequestParameters) : IRequest<(IEnumerable<ExpandoObject> users, MetaData metaData)>;
}
