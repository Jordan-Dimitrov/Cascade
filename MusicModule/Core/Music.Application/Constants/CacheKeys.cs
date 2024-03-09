using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Constants
{
    public static class CacheKeys
    {
        public const string AlbumsKey = "albums";
        public const string ArtistsKey = "artists";
        public static string GetAlbumKey(Guid id)
        {
            return $"album-{id}";
        }
        public static string GetArtistKey(Guid id)
        {
            return $"artist-{id}";
        }
    }
}
