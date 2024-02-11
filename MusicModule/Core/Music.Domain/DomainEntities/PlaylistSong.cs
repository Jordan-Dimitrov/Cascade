using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class PlaylistSong : Entity
    {
        private Guid _PlaylistId;
        private Guid _SongId;
        public PlaylistSong(Guid songId, Guid playlistId)
        {
            Id = Guid.NewGuid();
            SongId = songId;
            PlaylistId = playlistId;
        }

        [JsonConstructor]
        private PlaylistSong()
        {

        }

        public Guid PlaylistId
        {
            get
            {
                return _PlaylistId;
            }
            private set
            {
                _PlaylistId = value;
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
