using MediatR;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;

namespace Music.Application.Albums.Commands
{
    public sealed record SaveChangesForPatchCommand(AlbumPatchDto AlbumToPatch, Album Album) : IRequest;
}
