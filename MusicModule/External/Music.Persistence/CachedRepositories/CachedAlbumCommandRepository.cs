using Application.Shared.Abstractions;
using Music.Application.Constants;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.DomainEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.CachedRepositories
{
    internal sealed class CachedAlbumCommandRepository : IAlbumCommandRepository
    {
        private readonly IAlbumCommandRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedAlbumCommandRepository(IAlbumCommandRepository decorated, ICacheService cacheService)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
        }

        public async Task DeleteAsync(Album value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetAlbumKey(value.Id));
            await _Decorated.DeleteAsync(value);
        }

        public async Task InsertAsync(Album value)
        {
            await _Decorated.InsertAsync(value);
        }

        public async Task UpdateAsync(Album value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetAlbumKey(value.Id));
            await _Decorated.UpdateAsync(value);
        }
    }
}
