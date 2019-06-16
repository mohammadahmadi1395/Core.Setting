using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IRedisCacheService
    {
        Task AddAsync(string key, string value);
        Task<string> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}
