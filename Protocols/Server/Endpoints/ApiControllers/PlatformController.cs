using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Application.Platforms;
using Server.Application.Platforms.ViewModels.AddPlatform;
using Server.Domain;
using Server.Infrastructure;

namespace Server.Endpoints.ApiControllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformController : Controller
    {
        // https://nodogmablog.bryanhogan.net/2019/06/streaming-results-from-entity-framework-core-and-web-api-core/
        [HttpGet]
        public IQueryable<Platform> GetPlatforms([FromServices] AppDbContext appDbContext)
        {
            return appDbContext.Platforms.AsNoTracking();
        }

        [HttpGet]
        [Route("commands")]
        public IQueryable<Command> GetCommands([FromServices] AppDbContext appDbContext)
        {
            return appDbContext.Commands.AsNoTracking();
        }

        [HttpPost]
        public Task<AddPlatformResponse> AddPlatform(
            AddPlatformRequest request,
            [FromServices] PlatformAppService platformAppService,
            CancellationToken cancellationToken)
        {
            return platformAppService.AddPlatform(request, cancellationToken);
        }
    }
}
