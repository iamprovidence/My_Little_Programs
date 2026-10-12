using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using Server.Infrastructure;

namespace Server.Endpoints.MCP.Prompts
{
    // prompts are templates the user picks explicitly (e.g. as slash commands in the client)
    [McpServerPromptType]
    public class PlatformPrompts
    {
        private readonly AppDbContext _appDbContext;

        public PlatformPrompts(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [McpServerPrompt(Name = "explain_command")]
        [Description("Asks the model to explain what a CLI command does.")]
        public string ExplainCommand(
            [Description("Command line to explain, e.g. dotnet add.")] string commandLine)
        {
            return $"Explain what the command `{commandLine}` does, list its most useful options and give a short usage example.";
        }

        [McpServerPrompt(Name = "platform_cheat_sheet")]
        [Description("Builds a cheat sheet for a platform from the commands stored on the server.")]
        public async Task<IReadOnlyCollection<ChatMessage>> PlatformCheatSheet(
            [Description("Id of the platform.")] int platformId,
            CancellationToken cancellationToken)
        {
            var platform = await _appDbContext.Platforms
                .AsNoTracking()
                .Include(p => p.Commands)
                .FirstOrDefaultAsync(p => p.Id == platformId, cancellationToken);

            if (platform == null)
            {
                return new[]
                {
                    new ChatMessage(ChatRole.User, $"Platform with id {platformId} does not exist. Tell me that and suggest calling the get_platforms tool."),
                };
            }

            var commands = platform.Commands.Any()
                ? string.Join("\n", platform.Commands.Select(c => $"- `{c.CommandLine}` {c.Help}"))
                : "(no commands stored yet)";

            return new[]
            {
                new ChatMessage(ChatRole.User, $"Here are the known commands of the {platform.Name} platform:\n{commands}"),
                new ChatMessage(ChatRole.Assistant, "Got it. What should the cheat sheet focus on?"),
                new ChatMessage(ChatRole.User, $"Make a concise markdown cheat sheet for {platform.Name}: group the commands above by purpose, add a one line description to each and suggest a few important commands that are missing."),
            };
        }
    }
}
