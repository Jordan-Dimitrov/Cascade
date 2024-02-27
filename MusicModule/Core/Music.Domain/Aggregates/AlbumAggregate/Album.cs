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
        private AlbumName _AlbumName;
        private DateTime _DateCreated;
        private List<AlbumSong> _Songs;
        private Album(AlbumName albumName, DateTime dateCreated, List<AlbumSong> songs)
        {
            Id = Guid.NewGuid();
            AlbumName = albumName;
            Songs = songs;
        }

        [JsonConstructor]
        private Album()
        {

        }

        public static Album CreateAlbum(string albumName, DateTime dateCreated, Guid songId)
        {
            Album album = new Album(new AlbumName(albumName), DateTime.UtcNow, new List<AlbumSong>());

            album.Songs.Add(new AlbumSong(songId, album.Id));

            return album;
        }

        public void RemoveSong(Guid songId)
        {
            AlbumSong? song = _Songs.FirstOrDefault(x => x.SongId == songId);

            if (song is null)
            {
                throw new DomainValidationException("There is no such song to remove");
            }

            _Songs.Remove(song);
        }

        public void AddSong(Guid songId)
        {
            AlbumSong? song = _Songs.FirstOrDefault(x => x.SongId == songId);

            if (song is not null)
            {
                throw new DomainValidationException("There is already such song");
            }

            song = new AlbumSong(songId, Id);

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

        public Album HideAlbum()
        {
            this.AlbumName = new AlbumName("Hidden");
            this.Songs = new List<AlbumSong>();
            return this;
        }

        public List<AlbumSong> Songs
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
