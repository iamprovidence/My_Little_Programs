using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using Server.Application.Platforms;
using Server.Application.Platforms.ViewModels.AddPlatform;
using Server.Domain;
using Server.Infrastructure;

namespace Server.Endpoints.MCP.Tools
{
    // tools are actions the model can call on its own
    [McpServerToolType]
    public class PlatformTools
    {
        private readonly AppDbContext _appDbContext;
        private readonly PlatformAppService _platformAppService;

        public PlatformTools(
            AppDbContext appDbContext,
            PlatformAppService platformAppService)
        {
            _appDbContext = appDbContext;
            _platformAppService = platformAppService;
        }

        [McpServerTool(Name = "get_platforms", ReadOnly = true)]
        [Description("Returns all platforms (software with CLI).")]
        public async Task<IReadOnlyCollection<Platform>> GetPlatforms(CancellationToken cancellationToken)
        {
            return await _appDbContext.Platforms
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        [McpServerTool(Name = "get_commands", ReadOnly = true)]
        [Description("Returns CLI commands, optionally only for the given platform.")]
        public async Task<IReadOnlyCollection<Command>> GetCommands(
            CancellationToken cancellationToken,
            [Description("Id of the platform to filter by. Omit to get commands of all platforms.")] int? platformId = null)
        {
            var commands = _appDbContext.Commands.AsNoTracking();

            if (platformId.HasValue)
            {
                commands = commands.Where(c => c.PlatformId == platformId.Value);
            }

            return await commands.ToListAsync(cancellationToken);
        }

        [McpServerTool(Name = "add_platform", Destructive = false, Idempotent = false)]
        [Description("Creates a new platform and returns it.")]
        public Task<AddPlatformResponse> AddPlatform(
            [Description("Name of the platform, e.g. Docker.")] string name,
            CancellationToken cancellationToken)
        {
            return _platformAppService.AddPlatform(new AddPlatformRequest
            {
                Name = name,
            }, cancellationToken);
        }
    }
}
