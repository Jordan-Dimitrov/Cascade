using Application.Shared.Abstractions;
using Music.Application.Constants;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;

namespace Music.Persistence.CachedRepositories
{
    internal sealed class CachedArtistCommandRepository : IArtistCommandRepository
    {
        private readonly IArtistCommandRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedArtistCommandRepository(IArtistCommandRepository artistCommandRepository,
            ICacheService cacheService)
        {
            _Decorated = artistCommandRepository;
            _CacheService = cacheService;
        }
        public async Task DeleteAsync(Artist value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetArtistKey(value.Id));
            await _Decorated.DeleteAsync(value);
        }

        public async Task InsertAsync(Artist value)
        {
            await _Decorated.InsertAsync(value);
        }

        public async Task UpdateAsync(Artist value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetArtistKey(value.Id));
            await _Decorated.UpdateAsync(value);
        }
    }
}
