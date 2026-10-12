using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace Client.MCP
{
    internal class McpApiClient : IApiClient
    {
        // MCP listens on the HTTP/1.1 endpoint, the HTTP/2 only one is for gRPC
        // connecting is async (initialize handshake), so the field holds the task and calls await it
        private static readonly Task<McpClient> _mcpClient = McpClient.CreateAsync(
            new HttpClientTransport(new HttpClientTransportOptions
            {
                Endpoint = new Uri("http://localhost:38851/mcp"),
                TransportMode = HttpTransportMode.StreamableHttp,
            }));

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public async Task<IReadOnlyCollection<PlatformViewModel>> GetPlatforms()
        {
            return await CallTool<IReadOnlyCollection<PlatformViewModel>>("get_platforms", new Dictionary<string, object>(), CancellationToken.None);
        }

        public async Task<IReadOnlyCollection<CommandViewModel>> GetCommands()
        {
            return await CallTool<IReadOnlyCollection<CommandViewModel>>("get_commands", new Dictionary<string, object>(), CancellationToken.None);
        }

        public async Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken)
        {
            return await CallTool<AddPlatformResponse>("add_platform", new Dictionary<string, object>
            {
                ["name"] = request.Name,
            }, cancellationToken);
        }

        public IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken)
        {
            // the server publishes no MCP notifications about added platforms, so subscription is not supported
            return new NullDisposable();
        }

        // tool results come back as text content with the JSON the server serialized
        private static async Task<T> CallTool<T>(string name, IReadOnlyDictionary<string, object> arguments, CancellationToken cancellationToken)
        {
            var client = await _mcpClient;

            var result = await client.CallToolAsync(name, arguments, cancellationToken: cancellationToken);

            var text = string.Concat(result.Content.OfType<TextContentBlock>().Select(c => c.Text));

            if (result.IsError == true)
            {
                throw new InvalidOperationException($"MCP tool '{name}' failed: {text}");
            }

            return JsonSerializer.Deserialize<T>(text, _jsonOptions);
        }

        private class NullDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
