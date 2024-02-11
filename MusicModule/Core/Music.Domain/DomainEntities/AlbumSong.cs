using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class AlbumSong : Entity
    {
        private Guid _AlbumId;
        private Guid _SongId;
        public AlbumSong(Guid songId, Guid albumId)
        {
            Id = Guid.NewGuid();
            SongId = songId;
            AlbumId = albumId;
        }

        [JsonConstructor]
        private AlbumSong()
        {

        }

        public Guid AlbumId
        {
            get
            {
                return _AlbumId;
            }
            private set
            {
                _AlbumId = value;
            }
        }
        public Guid SongId
        {
            get
            {
                return _SongId;
            }
            private set
            {
                _SongId = value;
            }
        }
    }
}
