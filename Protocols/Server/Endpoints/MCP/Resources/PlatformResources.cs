using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using Server.Infrastructure;

namespace Server.Endpoints.MCP.Resources
{
    // resources are read-only data the client attaches to the context
    [McpServerResourceType]
    public class PlatformResources
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        private readonly AppDbContext _appDbContext;

        public PlatformResources(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [McpServerResource(UriTemplate = "platforms://all", Name = "platforms", MimeType = "application/json")]
        [Description("All platforms.")]
        public async Task<string> GetPlatforms(CancellationToken cancellationToken)
        {
            var platforms = await _appDbContext.Platforms
                .AsNoTracking()
                .Select(p => new { p.Id, p.Name })
                .ToListAsync(cancellationToken);

            return JsonSerializer.Serialize(platforms, JsonOptions);
        }

        [McpServerResource(UriTemplate = "platforms://{id}", Name = "platform", MimeType = "application/json")]
        [Description("Single platform with its commands.")]
        public async Task<string> GetPlatform(int id, CancellationToken cancellationToken)
        {
            // projection, because Platform <-> Command navigation properties make a cycle
            var platform = await _appDbContext.Platforms
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    Commands = p.Commands.Select(c => new { c.Id, c.CommandLine, c.Help }),
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (platform == null)
            {
                throw new McpProtocolException($"Platform with id {id} does not exist.", McpErrorCode.ResourceNotFound);
            }

            return JsonSerializer.Serialize(platform, JsonOptions);
        }

        [McpServerResource(UriTemplate = "commands://all", Name = "commands", MimeType = "application/json")]
        [Description("All CLI commands of all platforms.")]
        public async Task<string> GetCommands(CancellationToken cancellationToken)
        {
            var commands = await _appDbContext.Commands
                .AsNoTracking()
                .Select(c => new { c.Id, c.CommandLine, c.Help, c.PlatformId })
                .ToListAsync(cancellationToken);

            return JsonSerializer.Serialize(commands, JsonOptions);
        }
    }
}
