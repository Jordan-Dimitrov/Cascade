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

namespace Music.Domain.Aggregates.AlbumAggregate
{
    public sealed class Album : AggregateRoot
    {
        private const string _HiddenName = "Hidden";
        private const byte _AllowedSongCount = 16;
        private AlbumName _AlbumName;
        private DateTime _DateCreated;
        private List<Song> _Songs;
        private Guid _ArtistId;
        private Album(AlbumName albumName, DateTime dateCreated, List<Song> songs, Guid userId)
        {
            Id = Guid.NewGuid();
            AlbumName = albumName;
            DateCreated = dateCreated;
            Songs = songs;
            ArtistId = userId;
        }

        [JsonConstructor]
        private Album()
        {

        }

        public static Album CreateAlbum(string albumName, Guid userId, Song song)
        {
            Album album = new Album(new AlbumName(albumName), DateTime.UtcNow, new List<Song>(), userId);

            album.Songs.Add(song);

            return album;
        }

        public void RemoveSong(Guid songId)
        {
            if (_Songs is null)
            {
                throw new DomainValidationException("No songs to remove!");
            }
            Song? song = _Songs.FirstOrDefault(x => x.Id == songId);

            if (song is null)
            {
                throw new DomainValidationException("There is no such song to remove");
            }

            _Songs.Remove(song);
        }

        public void AddSong(Song song)
        {
            if (_Songs is null)
            {
                _Songs = new List<Song>();
            }
            if (_Songs.Count > _AllowedSongCount)
            {
                throw new ApplicationException("Cannot add more songs to this album");
            }
            if (_Songs.Any(x => x == song || x.SongName == song.SongName))
            {
                throw new DomainValidationException("There is already such song");
            }

            _Songs.Add(song);
        }

        public AlbumName AlbumName
        {
            get
            {
                return _AlbumName;
            }
            private set
            {
                _AlbumName = value;
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

        public void HideAlbum()
        {
            AlbumName = new AlbumName(_HiddenName);
            foreach (Song song in Songs)
            {
                song.HideSong();
            }
        }

        public List<Song> Songs
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
