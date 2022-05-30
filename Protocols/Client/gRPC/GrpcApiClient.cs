using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;
using Grpc.Net.Client;

namespace Client.gRPC
{
    internal class GrpcApiClient : IApiClient
    {
        private static readonly PlatformGrpc.PlatformGrpcClient _grpcClient = new PlatformGrpc.PlatformGrpcClient(GrpcChannel.ForAddress("http://localhost:38850"));

        public async Task<IReadOnlyCollection<PlatformViewModel>> GetPlatforms()
        {
            var response = await _grpcClient.GetPlatformsAsync(new GetPlatformsGrpcRequest());

            return response.Items.Select(x => new PlatformViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task<IReadOnlyCollection<CommandViewModel>> GetCommands()
        {
            var response = await _grpcClient.GetCommandsAsync(new GetCommandsGrpcRequest());

            return response.Items.Select(x => new CommandViewModel
            {
                Id = x.Id,
                CommandLine = x.CommandLine,
                Help = x.Help,
                PlatformId = x.PlatformId,
            }).ToList();
        }

        public async Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken)
        {
            var grpcRequest = new AddPlatformGrpcRequest
            {
                Name = request.Name,
            };

            var response = await _grpcClient.AddPlatformAsync(grpcRequest, cancellationToken: cancellationToken);

            return new AddPlatformResponse
            {
                Id = response.Id,
                Name = response.Name,
            };
        }

        public IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken)
        {
            var stream = _grpcClient.SubscribeToAddPlatform(new SubscribeToPlatformAddedGrpcStream(), cancellationToken: cancellationToken);

            while (stream.ResponseStream.MoveNext(cancellationToken).Result)
            {
                var grpcEvent = stream.ResponseStream.Current;

                action(new PlatformAddedEvent
                {
                    Id = grpcEvent.Id,
                    Name = grpcEvent.Name,
                });
            }

            return stream;
        }
    }
}
