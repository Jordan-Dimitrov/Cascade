using MediatR;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    public sealed record SaveChangesForPatchCommand(AlbumPatchDto AlbumToPatch, Album Album) : IRequest;
}
