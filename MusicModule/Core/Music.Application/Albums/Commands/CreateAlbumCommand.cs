using Domain.Shared.Constants;
using MediatR;
using Music.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
     public sealed record CreateAlbumCommand(CreateAlbumDto CreateAlbumDto, string JwtToken,
         byte[] File, string FileName) : IRequest<Guid>;
}
