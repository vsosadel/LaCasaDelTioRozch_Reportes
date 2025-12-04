using Infraestructura.DTO;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Caching.Memory;

namespace Infraestructura.Repository
{
    public class CacheRepository(IMemoryCache cache) : ICacheRepository
    {
        private readonly IMemoryCache _cache = cache;

        public async Task<CacheDTO> PorId(CacheDTO iCache)
        {
            iCache.Value = _cache.Get<string>(iCache.Key) ?? string.Empty;
            return await Task.FromResult(iCache);
        }

        public async Task Registra(CacheDTO iCache)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(3600));
            await Task.FromResult(_cache.Set(iCache.Key, iCache.Value, cacheEntryOptions));
        }
    }
}
