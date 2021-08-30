using System;

namespace DaCache.Abstractions.Models
{
	public class CacheEntryOptions
	{
		public CacheType Type { get; set; } = CacheType.Memory;
		public TimeSpan AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.FromHours(6);
		public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(30);
	}
}
