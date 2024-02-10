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

namespace Music.Domain.Aggregates.PlaylistAggregate
{
    public sealed class Playlist : AggregateRoot
    {
        private PlaylistName _PlaylistName;
        private DateTime _DateCreated;
        private List<PlaylistSong> _Songs;
        private Playlist(PlaylistName playlistName, DateTime dateCreated, List<PlaylistSong> songs)
        {
            Id = Guid.NewGuid();
            PlaylistName = playlistName;
            Songs = songs;
        }

        [JsonConstructor]
        private Playlist()
        {

        }

        public static Playlist CreatePlaylist(string playlistName, DateTime dateCreated, Guid songId)
        {
            Playlist playlist = new Playlist(new PlaylistName(playlistName), DateTime.UtcNow, new List<PlaylistSong>());

            playlist.Songs.Add(new PlaylistSong(songId, playlist.Id));

            return playlist;
        }



        public void RemoveSong(Guid songId)
        {
            PlaylistSong? song = _Songs.FirstOrDefault(x => x.SongId == songId);

            if (song is null)
            {
                throw new DomainValidationException("There is no such song to remove");
            }

            _Songs.Remove(song);
        }

        public void AddSong(Guid songId)
        {
            _Songs.Add(new PlaylistSong(songId, Id));
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

        public List<PlaylistSong> Songs
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
    }
}
