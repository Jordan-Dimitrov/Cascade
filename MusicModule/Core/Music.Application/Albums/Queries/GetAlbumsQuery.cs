using Domain.Shared.RequestFeatures;
using MediatR;
using Music.Application.Dtos;
using Music.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Queries
{
    public sealed record GetAlbumsQuery(AlbumParameters RequestParameters) : IRequest<(IEnumerable<ExpandoObject> albums, MetaData metaData)>;
}
