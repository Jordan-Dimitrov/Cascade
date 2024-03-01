using Domain.Shared.Exceptions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.DomainServices
{
    public sealed class AlbumService
    {
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        private readonly ISongCommandRepository _SongCommandRepository;
        private readonly ISongQueryRepository _SongQueryRepository;
        public AlbumService(IAlbumCommandRepository albumCommandRepository,
            ISongCommandRepository songCommandRepository,
            ISongQueryRepository songQueryRepository,
            IArtistCommandRepository artistCommandRepository)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _SongCommandRepository = songCommandRepository;
            _SongQueryRepository = songQueryRepository;
            _ArtistCommandRepository = artistCommandRepository;
        }
        public async Task CreateAlbum(Artist artist, Album album, Song song)
        {
            if(await _SongQueryRepository.ExistsAsync(x => x.SongName == song.SongName))
            {
                throw new DomainValidationException("Such song already exists in another album!");
            }

            if (artist.Albums.Any(x => x.AlbumId == album.Id))
            {
                throw new DomainValidationException("Cannot add same album!");
            }

            await _SongCommandRepository.InsertAsync(song);

            await _AlbumCommandRepository.InsertAsync(album);

            artist.Albums.Add(new ArtistAlbum(artist.Id, album.Id));

            await _ArtistCommandRepository.UpdateAsync(artist);
        }
    }
}
