using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class ArtistListener : Entity
    {
        private Guid _ArtistId;
        private Guid _ListenerId;
        public ArtistListener(Guid artistId, Guid listenerId)
        {
            Id = Guid.NewGuid();
            ArtistId = artistId;
            ListenerId = listenerId;
        }

        [JsonConstructor]
        private ArtistListener()
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
        public Guid ArtistId
        {
            get
            {
                return _ArtistId;
            }
            private set
            {
                _ArtistId = value;
            }
        }
    }
}
