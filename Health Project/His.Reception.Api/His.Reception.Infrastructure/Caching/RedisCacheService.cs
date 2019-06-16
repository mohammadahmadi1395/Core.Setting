using His.Reception.Application.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Infrastructure.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService( IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task AddAsync(string key, string value)
        {
           await _distributedCache.SetStringAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
           return await _distributedCache.GetStringAsync(key);

        }

        public async Task RemoveAsync(string key)
        {
           await _distributedCache.RemoveAsync(key);
        }

    }
}
