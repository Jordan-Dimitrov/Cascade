using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.PlaylistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.DomainEntities;
using Music.Domain.ValueObjects;
using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.Aggregates.ListenerAggregate
{
    public sealed class Listener : AggregateRoot
    {
        private Username _Username;
        private List<ListenerPlaylist> _Playlists;
        private List<ArtistListener> _Artists;
        private Listener(Username username, List<ListenerPlaylist> playlists, List<ArtistListener> artists, Guid id)
        {
            Id = id;
            Username = username;
            Playlists = playlists;
            Artists = artists;
        }

        [JsonConstructor]
        private Listener()
        {

        }

        public static Listener CreateListener(string username, Guid id)
        {
            Listener listener = new Listener(new Username(username), new List<ListenerPlaylist>(), new List<ArtistListener>(), id);

            return listener;
        }

        public void AddPlaylist(Guid playlistId)
        {
            if (_Playlists.FirstOrDefault(x => x.PlaylistId == playlistId) is not null)
            {
                throw new DomainValidationException("Album already exists");
            }

            _Playlists.Add(new ListenerPlaylist(Id, playlistId));
        }

        public void RemoveAlbum(Guid playlistId)
        {
            ListenerPlaylist? playlist = _Playlists.FirstOrDefault(x => x.PlaylistId == playlistId);

            if (playlist is null)
            {
                throw new DomainValidationException("There is no such playlist to remove");
            }

            _Playlists.Remove(playlist);
        }
        public void AddArtist(Guid artistId)
        {
            if(_Artists.FirstOrDefault(x => x.ArtistId == artistId) is not null)
            {
                throw new DomainValidationException("Artist already exists in listeners favorites");
            }

            _Artists.Add(new ArtistListener(artistId, Id));
        }

        public void RemoveArtist(Guid artistId)
        {
            ArtistListener? artist = _Artists.FirstOrDefault(x => x.ArtistId == artistId);

            if (artist is null)
            {
                throw new DomainValidationException("No such artist to remove");
            }

            artist = new ArtistListener(artistId, Id);

            _Artists.Add(artist);
        }

        public Username Username
        {
            get
            {
                return _Username;
            }
            private set
            {
                _Username = value;
            }
        }

        public List<ArtistListener> Artists
        {
            get
            {
                return _Artists.AsReadOnly().ToList();
            }
            private set
            {
                _Artists = value;
            }
        }

        public List<ListenerPlaylist> Playlists
        {
            get
            {
                return _Playlists.AsReadOnly().ToList();
            }
            private set
            {
                _Playlists = value;
            }
        }

        public void HideListener()
        {
            Username = new Username("Hidden");
        }
    }
}
