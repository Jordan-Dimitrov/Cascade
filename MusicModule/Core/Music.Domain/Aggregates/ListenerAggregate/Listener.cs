using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.DomainEntities;
using Music.Domain.ValueObjects;
using Shared.Exceptions;
using Shared.Primitives;
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
        private List<Playlist> _Playlists;
        private List<Guid> _Artists;
        private Listener(Username username, List<Playlist> playlists, List<Guid> artists)
        {
            Id = Guid.NewGuid();
            Username = username;
            Playlists = playlists;
            Artists = artists;
        }

        [JsonConstructor]
        private Listener()
        {

        }

        public static Listener CreateListener(string username)
        {
            Listener listener = new Listener(new Username(username), new List<Playlist>(), new List<Guid>());

            return listener;
        }

        public void AddPlaylist(Playlist playlist)
        {
            if (_Playlists.FirstOrDefault(x => x.Id == playlist.Id) is not null)
            {
                throw new DomainValidationException("Album already exists");
            }

            if (SongExists(playlist.Songs.FirstOrDefault()))
            {
                throw new DomainValidationException("Song already exists in another album");
            }

            _Playlists.Add(playlist);
        }

        public void AddArtist(Guid artistId)
        {
            if(_Artists.Contains(artistId))
            {
                throw new DomainValidationException("Artist already exists in listeners favorites");
            }

            _Artists.Add(artistId);
        }

        private bool SongExists(Guid songId)
        {
            if (_Playlists.Where(x => x.Songs.Contains(songId)).FirstOrDefault() is not null)
            {
                return true;
            }

            return false;
        }

        public void RemoveAlbum(Playlist playlist)
        {
            if (_Playlists.FirstOrDefault(x => x.Id == playlist.Id) is null)
            {
                throw new DomainValidationException("Album does not exist");
            }

            _Playlists.Remove(playlist);
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

        public List<Guid> Artists
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

        public List<Playlist> Playlists
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

        }
    }
}
