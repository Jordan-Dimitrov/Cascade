using Application.Shared.Abstractions;
using Domain.Shared.Wrappers;
using Infrastructure.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Infrastructure.Shared.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _DistributedCache;
        private readonly ConcurrentDictionary<string, bool> _CacheKeys;
        private readonly DistributedCacheEntryOptions _CacheEntryOptions;
        private readonly CacheSettings _CacheSettings;
        public CacheService(IDistributedCache distributedCache, IOptions<CacheSettings> options)
        {
            _DistributedCache = distributedCache;
            _CacheKeys = new ConcurrentDictionary<string, bool>();
            _CacheEntryOptions = new DistributedCacheEntryOptions();
            _CacheSettings = options.Value;

            _CacheEntryOptions.SlidingExpiration = TimeSpan
                .FromSeconds(_CacheSettings.SlidingExpirationSeconds);

            _CacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan
                .FromMinutes(_CacheSettings.AbsoluteExpirationRelativeToNowMinutes);
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
            where T : class
        {
            string? cachedValue = await _DistributedCache.GetStringAsync(key, cancellationToken);

            if (cachedValue is null)
            {
                return null;
            }

            T? value = JsonConvert.DeserializeObject<T>(
                cachedValue,
                new JsonSerializerSettings
                {
                    ConstructorHandling =
                        ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
        {
            T? cachedValue = await GetAsync<T>(key, cancellationToken);

            if (cachedValue is not null)
            {
                return cachedValue;
            }

            cachedValue = await factory();

            await SetAsync(key, cachedValue, _CacheEntryOptions, cancellationToken);

            return cachedValue;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _DistributedCache.RemoveAsync(key, cancellationToken);

            _CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
            foreach (var key in _CacheKeys.Keys)
            {
                if (key.StartsWith(prefixKey))
                {
                    await RemoveAsync(key, cancellationToken);
                }
            }

            IEnumerable<Task> tasks = _CacheKeys.Keys
                .Where(x => x.StartsWith(prefixKey))
                .Select(x => RemoveAsync(x, cancellationToken));

            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value,
            DistributedCacheEntryOptions? options = default,
            CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);

            await _DistributedCache
                .SetStringAsync(key, cacheValue,
                options is null ? _CacheEntryOptions : options,
                cancellationToken);

            _CacheKeys.TryAdd(key, true);
        }
    }
}
