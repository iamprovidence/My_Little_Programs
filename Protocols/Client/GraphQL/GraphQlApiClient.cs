using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;
using Client.GraphQL.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace Client.GraphQL
{
    internal class GraphQlApiClient : IApiClient
    {
        // server accepts only HTTP/2 (see Kestrel:EndpointDefaults:Protocols), so over plain http we have to force h2c
        private static readonly HttpClient _httpClient = new HttpClient(new Http2Handler())
        {
            BaseAddress = new Uri("http://localhost:38850/graphql/"),
            DefaultRequestVersion = HttpVersion.Version20,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact,
        };
        private static readonly GraphQLHttpClient _graphQLClient = new GraphQLHttpClient(
            new GraphQLHttpClientOptions { EndPoint = new Uri("http://localhost:38850/graphql/") },
            new NewtonsoftJsonSerializer(),
            _httpClient);

        public async Task<IReadOnlyCollection<PlatformViewModel>> GetPlatforms()
        {
            // custom implementation
            var query = new
            {
                query = @"{platforms {id name} }",
                variables = new { },
            };

            var json = JsonSerializer.Serialize(query);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var message = await _httpClient.PostAsync(string.Empty, content);

            var response = await message.Content.ReadAsStringAsync();

            var graphQlData = JsonSerializer.Deserialize<GqlData>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            var dataPayload = graphQlData.Data.GetProperty("platforms");

            return dataPayload.Deserialize<IReadOnlyCollection<PlatformViewModel>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public async Task<IReadOnlyCollection<CommandViewModel>> GetCommands()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                {
                    commands 
                    {
                        id,
                        help,
                        commandLine,
                        platformId
                    }
                }"
            };

            var response = await _graphQLClient.SendQueryAsync<CommandsResponse>(request);

            return response.Data.Commands;
        }

        public async Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = $@"                
                mutation {{
                    addPlatform(request: {{
                    name: ""{request.Name}""
                    }})
                    {{
                        id,
                        name
                    }}
                }}",
            };

            var response = await _graphQLClient.SendMutationAsync<AddPlatformGraphQlResponse>(graphQlRequest, cancellationToken);

            return response.Data.AddPlatform;
        }

        public IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken)
        {
            var platformAddedSubscription = new GraphQLRequest
            {
                Query = @"
                    subscription {
                        onPlatformAdded {
                            id,
                            name
                        }
                    }"
            };

            var subscriptionStream = _graphQLClient.CreateSubscriptionStream<PlatformAddedGraphQlEvent>(platformAddedSubscription);

            return subscriptionStream.Subscribe(onNext: data =>
            {
                action(data.Data.OnPlatformAdded);
            });
        }

        // GraphQLHttpClient builds its own HttpRequestMessage (HTTP/1.1 by default) and ignores HttpClient.DefaultRequestVersion
        private class Http2Handler : DelegatingHandler
        {
            public Http2Handler()
                : base(new HttpClientHandler())
            {
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Version = HttpVersion.Version20;
                request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
