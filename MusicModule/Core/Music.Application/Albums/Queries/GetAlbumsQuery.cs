using Domain.Shared.RequestFeatures;
using MediatR;
using Music.Domain.RequestFeatures;
using System.Dynamic;

namespace Music.Application.Albums.Queries
{
    public sealed record GetAlbumsQuery(AlbumParameters RequestParameters) : IRequest<(IEnumerable<ExpandoObject> albums, MetaData metaData)>;
}
