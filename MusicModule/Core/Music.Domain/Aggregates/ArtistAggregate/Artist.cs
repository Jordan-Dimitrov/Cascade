using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Music.Domain.Aggregates.ArtistAggregate
{
    public sealed class Artist : AggregateRoot
    {
        private const string _HiddenName = "Hidden";
        private Username _Username;
        private List<Album> _Albums;
        private Artist(Username username, List<Album> albums, Guid id)
        {
            Id = id;
            Username = username;
        }

        [JsonConstructor]
        private Artist()
        {

        }

        public static Artist CreateArtist(string username, Guid id)
        {
            Artist artist = new Artist(new Username(username), new List<Album>(), id);

            return artist;
        }

        public void AddAlbum(Album album)
        {
            if (_Albums is null)
            {
                _Albums = new List<Album>();
            }

            if (_Albums.Any(x => x == album))
            {
                throw new DomainValidationException("Album already exists");
            }

            _Albums.Add(album);
        }

        public void RemoveAlbum(Guid albumId)
        {
            Album? album = _Albums.FirstOrDefault(x => x.Id == albumId);

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

        public List<Album> Albums
        {
            get
            {
                if (_Albums is null)
                {
                    return null;
                }
                return _Albums.AsReadOnly().ToList();
            }
            private set
            {
                _Albums = value;
            }
        }

        public void HideArtist()
        {
            Username = new Username(_HiddenName);
        }
    }
}
