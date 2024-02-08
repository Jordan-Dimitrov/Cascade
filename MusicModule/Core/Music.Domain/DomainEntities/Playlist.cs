using Music.Domain.ValueObjects;
using Shared.Exceptions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.DomainEntities
{
    public sealed class Playlist : Entity
    {
        private PlaylistName _PlaylistName;
        private DateTime _DateCreated;
        private List<Guid> _Songs;
        private Guid _UserId;
        private Playlist(PlaylistName playlistName, DateTime dateCreated, Guid userId, List<Guid> songs)
        {
            Id = Guid.NewGuid();
            PlaylistName = playlistName;
            Songs = songs;
            UserId = userId;
        }

        [JsonConstructor]
        private Playlist()
        {

        }

        public static Playlist CreatePlaylist(string playlistName, DateTime dateCreated, Guid songId, Guid UserId)
        {
            Playlist playlist = new Playlist(new PlaylistName(playlistName), DateTime.UtcNow, UserId, new List<Guid>() { songId });

            return playlist;
        }

        public void RemoveSong(Guid songId)
        {
            if (!_Songs.Contains(songId))
            {
                throw new DomainValidationException("There is no such song to remove");
            }

            _Songs.Remove(songId);
        }

        public void AddSong(Guid songId)
        {
            _Songs.Add(songId);
        }

        public PlaylistName PlaylistName
        {
            get
            {
                return _PlaylistName;
            }
            private set
            {
                _PlaylistName = value;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            private set
            {
                _DateCreated = value;
            }
        }

        public List<Guid> Songs
        {
            get
            {
                return _Songs.AsReadOnly().ToList();
            }
            private set
            {
                _Songs = value;
            }
        }

        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            private set
            {
                _UserId = value;
            }
        }
    }
}
