using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Server.Application.Platforms;
using Server.Application.Platforms.ViewModels.AddPlatform;
using Server.Infrastructure;

namespace Server.Endpoints.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformResponse> AddPlatform(
            AddPlatformRequest request,
            [Service] PlatformAppService platformAppService,
            CancellationToken cancellationToken)
        {
            return await platformAppService.AddPlatform(request, cancellationToken);
        }
    }
}
