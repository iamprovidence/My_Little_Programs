using DaCache.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace DaCache.Abstractions.Interfaces
{
	public interface ICacheService
	{
		Task<T> GetOrAddItem<T>(string key, Func<CacheEntryOptions, Task<T>> buildCacheEntryValue)
			where T : class;
		Task UpdateItem<T>(string key, T item, CacheEntryOptions cacheEntryOptions)
			where T : class;
		Task Remove(string key);
	}
}
