using Application.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.IntegrationEvents
{
    public sealed record SongCreatedIntegrationEvent(string FileName, byte[] File, string[] Lyrics)
        : IntegragtionEvent;
}
