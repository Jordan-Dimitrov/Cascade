using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
