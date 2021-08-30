using DaCache.Abstractions.Models;

namespace DaCache.Abstractions.Services
{
	public static class CacheItemKeyHelper
	{
		public static string GetKey(CacheItemType itemType, int tenantId, int? itemId = null)
		{
			return $"tenantId#{tenantId}:type#{itemType}:item#{itemId}";
		}
	}
}
