using DaCache.Abstractions.Interfaces;
using DaCache.Abstractions.Models;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaCache.Implementation.Telemetry
{
	internal class CacheServiceTrackingDecorator : TrackingDecoratorBase, ICacheService
	{
		private readonly ICacheService _cacheService;

		public CacheServiceTrackingDecorator(
			ICacheService cacheService,
			TelemetryClient telemetryClient,
			TrackingOptions trackingOptions)
			: base(telemetryClient, trackingOptions)
		{
			_cacheService = cacheService;
		}

		public Task<T> GetOrAddItem<T>(string key, Func<CacheEntryOptions, Task<T>> buildCacheEntryValue)
			where T : class
		{
			return ExecuteWithTracking(nameof(GetOrAddItem), () =>
			{
				return _cacheService.GetOrAddItem(key, buildCacheEntryValue);
			}, CreateProperties(key));
		}

		public Task UpdateItem<T>(string key, T item, CacheEntryOptions cacheEntryOptions)
			where T : class
		{
			return ExecuteWithTracking(nameof(UpdateItem), async () =>
			{
				await _cacheService.UpdateItem(key, item, cacheEntryOptions);

				return await Task.FromResult<T>(default);
			}, CreateProperties(key));
		}

		public Task Remove(string key)
		{
			return ExecuteWithTracking<object>(nameof(Remove), async () =>
			{
				await _cacheService.Remove(key);

				return Task.FromResult<object>(default);
			}, CreateProperties(key));
		}

		private IDictionary<string, string> CreateProperties(string key)
		{
			return new Dictionary<string, string>()
			{
				["Key"] = key,
			};
		}
	}
}
