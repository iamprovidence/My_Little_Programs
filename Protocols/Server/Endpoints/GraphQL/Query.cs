using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Server.Domain;
using Server.Infrastructure;

namespace Server.Endpoints.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))] // parallel execution
        [UseProjection] // to pull child objects
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext appDbContext)
        {
            return appDbContext.Platforms;
        }


        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext appDbContext)
        {
            return appDbContext.Commands;
        }
    }
}
