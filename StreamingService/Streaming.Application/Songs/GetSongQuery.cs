using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Application.Songs
{
    public sealed record GetSongQuery(string FileName) : IRequest<(FileStream, string)>;
}
