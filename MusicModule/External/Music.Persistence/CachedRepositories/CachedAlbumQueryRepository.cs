﻿using Application.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using Music.Application.Constants;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.RequestFeatures;
using System.Linq.Expressions;

namespace Music.Persistence.CachedRepositories
{
    internal sealed class CachedAlbumQueryRepository : IAlbumQueryRepository
    {
        private readonly IAlbumQueryRepository _Decorated;
        private readonly ApplicationDbContext _Context;
        private readonly ICacheService _CacheService;
        public CachedAlbumQueryRepository(IAlbumQueryRepository decorated,
            ICacheService cacheService,
            ApplicationDbContext context)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
            _Context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Album, bool>> condition)
        {
            return await _Decorated.ExistsAsync(condition);
        }

        public async Task<PagedList<Album>> GetAlbumsWithPaginationAsync(AlbumParameters albumParameters,
            bool trackChanges)
        {
            return await _Decorated
                    .GetAlbumsWithPaginationAsync(albumParameters, trackChanges);
        }

        public async Task<ICollection<Album>> GetAllAsync(bool trackChanges)
        {
            return await _CacheService.GetAsync(CacheKeys.AlbumsKey,
                async () =>
                {
                    return await _Decorated
                    .GetAllAsync(trackChanges);
                });
        }

        public async Task<Album?> GetByIdAsync(Guid id, bool trackChanges)
        {
            Album? album = await _CacheService.GetAsync(CacheKeys.GetAlbumKey(id),
                async () =>
                {
                    return await _Decorated
                    .GetByIdAsync(id, trackChanges);
                });

            if (album is not null)
            {
                _Context.Set<Album>().Attach(album);
            }

            return album;
        }

        public async Task<Album?> GetByNameAsync(string username)
        {
            return await _Decorated.GetByNameAsync(username);
        }
    }
}
