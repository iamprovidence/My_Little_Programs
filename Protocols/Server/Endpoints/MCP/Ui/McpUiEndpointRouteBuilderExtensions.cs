using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Server.Endpoints.MCP.Ui
{
    public static class McpUiEndpointRouteBuilderExtensions
    {
        // simplified MCP Inspector, the same idea as GraphQL Voyager / Banana Cake Pop; the page talks to /mcp
        public static IEndpointConventionBuilder MapMcpUi(this IEndpointRouteBuilder endpoints, string pattern)
        {
            return endpoints.MapGet(pattern, async context =>
            {
                await using var page = typeof(McpUiEndpointRouteBuilderExtensions).Assembly.GetManifestResourceStream("mcp-ui.html")!;

                context.Response.ContentType = "text/html; charset=utf-8";
                await page.CopyToAsync(context.Response.Body, context.RequestAborted);
            });
        }
    }
}
