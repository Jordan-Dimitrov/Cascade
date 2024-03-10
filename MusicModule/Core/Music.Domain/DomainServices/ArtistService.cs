using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;

namespace Music.Domain.DomainServices
{
    public sealed class ArtistService
    {
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        public ArtistService(IAlbumCommandRepository albumCommandRepository,
            IArtistCommandRepository artistCommandRepository)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _ArtistCommandRepository = artistCommandRepository;
        }
        public async Task HideArtist(Artist artist)
        {
            foreach (var album in artist.Albums)
            {
                album.HideAlbum();
                await _AlbumCommandRepository.UpdateAsync(album);
            }

            artist.HideArtist();
            await _ArtistCommandRepository.UpdateAsync(artist);
        }
    }
}
