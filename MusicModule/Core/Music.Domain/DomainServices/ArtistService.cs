using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.DomainServices
{
    public sealed class ArtistService
    {
        public void HideArtist(Artist artist, IQueryable<Album> albums, IQueryable<Song> songs)
        {
            artist.HideArtist();

            albums.Select(x => x.HideAlbum());

            songs.Select(x => x.HideSong());
        }

    }
}
