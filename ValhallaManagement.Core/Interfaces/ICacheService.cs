using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaManagement.Core.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetCachedDataAsync<T>(string key);
        Task SetCachedDataAsync<T>(string key, T data, TimeSpan timeToLive);
        Task RemoveCachedDataAsync(string key);
    }
}
