using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Server.Domain;
using Server.Infrastructure;

namespace Server.Endpoints.GraphQL.Infrastructure
{
    // FluentAPI
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor
                .Description("Software with CLI");

            /*
            descriptor
                .Field(x => x.Name)
                .Ignore();
            */

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<CommandResolver>(p => p.GetCommands(default, default))
                .UseDbContext<AppDbContext>()
                .Description("List of availables commanands dor this platform");
        }

        // instead of [UseProjection] explicitly specify relation
        // [UseProjection] should be removed
        private class CommandResolver
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext appDbContext)
            {
                return appDbContext.Commands.Where(c => c.PlatformId == platform.Id);
            }
        }
    }
}
