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
    public sealed class Album : Entity
    {
        private AlbumName _AlbumName;
        private DateTime _DateCreated;
        private List<Guid> _Songs;
        private Guid _UserId;
        private Album(AlbumName albumName, DateTime dateCreated, Guid userId, List<Guid> songs)
        {
            Id = Guid.NewGuid();
            AlbumName = albumName;
            Songs = songs;
            UserId = userId;
        }

        [JsonConstructor]
        private Album()
        {

        }

        public static Album CreateAlbum(string albumName, DateTime dateCreated, Guid songId, Guid UserId)
        {
            Album album = new Album(new AlbumName(albumName), DateTime.UtcNow, UserId, new List<Guid>() { songId });

            return album;
        }

        public void RemoveSong(Guid songId)
        {
            if(!_Songs.Contains(songId)) 
            {
                throw new DomainValidationException("There is no such song to remove");
            }

            _Songs.Remove(songId);
        }

        public void AddSong(Guid songId)
        {
            if (_Songs.Contains(songId))
            {
                throw new DomainValidationException("Song already exists");
            }

            _Songs.Add(songId);
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
