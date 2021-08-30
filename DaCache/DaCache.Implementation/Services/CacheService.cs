using DaCache.Abstractions.Interfaces;
using DaCache.Abstractions.Models;
using DaCache.Implementation.Interfaces;
using DaCache.Implementation.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DaCache.Implementation.Services
{
	internal class CacheService : ICacheService
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IDistributedCache _distributedCache;
		private readonly IConnectionMultiplexer _connectionMultiplexer;
		private readonly ISerializer _serializer;

		public CacheService(
			IMemoryCache memoryCache,
			IDistributedCache distributedCache,
			IConnectionMultiplexer connectionMultiplexer,
			ISerializer serializer)
		{
			_memoryCache = memoryCache;
			_distributedCache = distributedCache;
			_connectionMultiplexer = connectionMultiplexer;
			_serializer = serializer;

			StartConsumingEvents();
		}
		
		private void StartConsumingEvents()
		{
			var subscriber = _connectionMultiplexer.GetSubscriber();

			subscriber.Subscribe(nameof(MemoryCacheClearedEvent), (channel, message) =>
			{
				var eventMessage = JsonConvert.DeserializeObject<MemoryCacheClearedEvent>(message);
				HandleEvent(eventMessage);
			});
		}

		public async Task<T> GetOrAddItem<T>(string key, Func<CacheEntryOptions, Task<T>> buildCacheEntryValue) where T : class
		{
			var serializedValue = _memoryCache.Get<byte[]>(key) ?? await _distributedCache.GetAsync(key);
			if (serializedValue != null)
			{
				return await _serializer.Deserialize<T>(serializedValue);
			}

			var cacheEntryOptions = new CacheEntryOptions();
			var value = await buildCacheEntryValue(cacheEntryOptions);
			await PutItemToCache(key, value, cacheEntryOptions);

			return value;
		}

		public Task UpdateItem<T>(string key, T item, CacheEntryOptions cacheEntryOptions) where T : class
		{
			return PutItemToCache(key, item, cacheEntryOptions);
		}

		private async Task PutItemToCache<T>(string key, T item, CacheEntryOptions cacheEntryOptions)
			where T : class
		{
			var serializedObject = await _serializer.Serialize(item);

			if (cacheEntryOptions.Type == CacheType.Distributed)
			{
				await _distributedCache.SetAsync(key, serializedObject, new DistributedCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = cacheEntryOptions.AbsoluteExpirationRelativeToNow,
					SlidingExpiration = cacheEntryOptions.SlidingExpiration,
				});
			}
			else if (cacheEntryOptions.Type == CacheType.Memory)
			{
				_memoryCache.Set(key, serializedObject, new MemoryCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = cacheEntryOptions.AbsoluteExpirationRelativeToNow,
					SlidingExpiration = cacheEntryOptions.SlidingExpiration,
				});
			}
		}

		public async Task Remove(string key)
		{
			await RemoveMemoryCache(key);
			await RemoveDistributedCache(key);
		}

		private async Task RemoveMemoryCache(string key)
		{
			var subscribers = _connectionMultiplexer.GetSubscriber();

			var eventMessage = new MemoryCacheClearedEvent
			{
				Key = key,
			};

			// publish message, so each instance clear its own memory
			await subscribers
				.PublishAsync(nameof(MemoryCacheClearedEvent), JsonConvert.SerializeObject(eventMessage), CommandFlags.FireAndForget);
		}

		private void HandleEvent(MemoryCacheClearedEvent eventMessage)
		{
			_memoryCache.Remove(eventMessage.Key);
		}

		private async Task RemoveDistributedCache(string key)
		{
			var cacheDb = _connectionMultiplexer.GetDatabase();

			// clear each shard
			foreach (var endPoints in _connectionMultiplexer.GetEndPoints())
			{
				var server = _connectionMultiplexer.GetServer(endPoints);
				var keys = server.Keys(pattern: key).ToArray();

				await cacheDb.KeyDeleteAsync(keys);
			}
		}
	}
}
