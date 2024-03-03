﻿using MediatR;
using Music.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    public sealed record AddSongToAlbumCommand(CreateSongDto CreateSongDto, string JwtToken,
         byte[] File, string FileName, Guid AlbumId) : IRequest;
}