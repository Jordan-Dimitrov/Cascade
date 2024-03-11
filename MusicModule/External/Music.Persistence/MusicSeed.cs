using Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;

namespace Music.Persistence
{
    public class MusicSeed
    {
        private readonly ApplicationDbContext _Context;
        public MusicSeed(ApplicationDbContext context)
        {
            _Context = context;
        }
        public void SeedApplicationContext()
        {
            _Context.Database.Migrate();

            if (!_Context.Artists.Any())
            {
                List<Artist> artists = new List<Artist>();

                Artist artist = Artist.CreateArtist("KristiQn Enchev", Guid.NewGuid());
                artist.AddAlbum(Album.CreateAlbum("Emotivna Luda",
                    artist.Id, Song.CreateSong("Rodjen sa Greskom", "hiddengg.ogg",
                    "Srubsko", "ABCDEFGH"), new string[2], "hiddengg.ogg"));

                artists.Add(artist);

                _Context.Artists.AddRange(artists);
                _Context.SaveChanges();
            }
        }
    }
}
