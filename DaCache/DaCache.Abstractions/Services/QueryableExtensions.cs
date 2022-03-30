using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DaCache.Abstractions.Models;
using DaCache.Abstractions.Interfaces;

namespace DaCache.Abstractions.Services
{
    public static class QueryableExtensions
    {
        public static Task<List<T>> ToCachedList<T>(this IQueryable<T> query, ICacheService cacheService, CacheEntryOptions options = null)
        {
            return cacheService.GetOrAddItem(query.ToQueryString(), cacheOptions =>
            {
                if (options is not null)
                {
                    cacheOptions.Type = options.Type;
                    cacheOptions.AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow;
                    cacheOptions.SlidingExpiration = options.SlidingExpiration;
                }

                return query.ToListAsync();
            });
        }
    }
}
