using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class ListenerPlaylist : Entity
    {
        private Guid _ListenerId;
        private Guid _PlaylistId;
        public ListenerPlaylist(Guid listenerId, Guid playlistId)
        {
            Id = Guid.NewGuid();
            ListenerId = listenerId;
            PlaylistId = playlistId;
        }

        [JsonConstructor]
        private ListenerPlaylist()
        {

        }

        public Guid ListenerId
        {
            get
            {
                return _ListenerId;
            }
            private set
            {
                _ListenerId = value;
            }
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
    }
}
