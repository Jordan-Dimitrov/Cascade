using Music.Domain.Aggregates.AlbumAggregate;
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

namespace Music.Domain.Aggregates.ArtistAggregate
{
    public sealed class Artist : AggregateRoot
    {
        private Username _Username;
        private FollowCount _FollowCount;
        private List<ArtistAlbum> _Albums;
        private Artist(Username username, FollowCount followCount, List<ArtistAlbum> albums, Guid id)
        {
            Id = id;
            Username = username;
            FollowCount = followCount;
            Albums = albums;
        }

        [JsonConstructor]
        private Artist()
        {

        }

        public static Artist CreateArtist(Guid id, string username)
        {
            Artist artist = new Artist(new Username(username), new FollowCount(0), new List<ArtistAlbum>(), id);

            return artist;
        }

        public void AddAlbum(Guid albumId)
        {
            if (_Albums.FirstOrDefault(x => x.AlbumId == albumId) is not null)
            {
                throw new DomainValidationException("Album already exists");
            }

            _Albums.Add(new ArtistAlbum(Id, albumId));
        }

        public void RemoveAlbum(Guid albumId)
        {
            ArtistAlbum? album = _Albums.FirstOrDefault(x => x.AlbumId == albumId);

            if (album is null)
            {
                throw new DomainValidationException("There is no such album to remove");
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

        public List<ArtistAlbum> Albums
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
