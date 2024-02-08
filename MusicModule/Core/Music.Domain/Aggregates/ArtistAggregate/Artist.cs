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

namespace Music.Domain.Aggregates.ArtistAggregate
{
    public sealed class Artist : AggregateRoot
    {
        private Username _Username;
        private FollowCount _FollowCount;
        private List<Album> _Albums;
        private Artist(Username username, FollowCount followCount, List<Album> albums)
        {
            Id = Guid.NewGuid();
            Username = username;
            FollowCount = followCount;
            Albums = albums;
        }

        [JsonConstructor]
        private Artist()
        {

        }

        public static Artist CreateArtist(string username)
        {
            Artist artist = new Artist(new Username(username), new FollowCount(0), new List<Album>());

            return artist;
        }

        public void AddAlbum(Album album)
        {
            if (_Albums.FirstOrDefault(x => x.Id == album.Id) is not null)
            {
                throw new DomainValidationException("Album already exists");
            }

            if (SongExists(album.Songs.FirstOrDefault()))
            {
                throw new DomainValidationException("Song already exists in another album");
            }

            _Albums.Add(album);
        }

        private bool SongExists(Guid songId)
        {
            if (_Albums.Where(x => x.Songs.Contains(songId)).FirstOrDefault() is not null)
            {
                return true;
            }

            return false;
        }

        public void RemoveAlbum(Album album)
        {
            if (_Albums.FirstOrDefault(x => x.Id == album.Id) is null)
            {
                throw new DomainValidationException("Album does not exist");
            }

            _Albums.Remove(album);
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

        public FollowCount FollowCount
        {
            get
            {
                return _FollowCount;
            }
            set 
            {
                _FollowCount = value; 
            }
        }

        public List<Album> Albums
        {
            get
            {
                return _Albums.AsReadOnly().ToList();
            }
            private set
            {
                _Albums = value;
            }
        }

        public void HideArtist()
        {

        }
    }
}
