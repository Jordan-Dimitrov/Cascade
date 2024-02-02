using Application.Dtos;
using Domain.Abstractions;
using Domain.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public sealed record GetUsersQuery(UserParameters RequestParameters) : IRequest<(IEnumerable<UserDto> users, MetaData metaData)>;
}
