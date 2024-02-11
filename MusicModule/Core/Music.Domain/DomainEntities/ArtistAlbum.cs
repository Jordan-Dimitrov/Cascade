using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class ArtistAlbum : Entity
    {
        private Guid _ArtistId;
        private Guid _AlbumId;
        public ArtistAlbum(Guid artistId, Guid albumId)
        {
            Id = Guid.NewGuid();
            ArtistId = artistId;
            AlbumId = albumId;
        }

        [JsonConstructor]
        private ArtistAlbum()
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
