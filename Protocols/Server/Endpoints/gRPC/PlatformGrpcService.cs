using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Server.Application.Platforms;
using Server.Application.Platforms.ViewModels.AddPlatform;
using Server.Domain;
using Server.Endpoints.gRPC.Infrastructure;
using Server.Infrastructure;

namespace Server.Endpoints.gRPC
{
    // code first approach?
    public class PlatformGrpcService : PlatformGrpc.PlatformGrpcBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly PlatformAppService _platformAppService;
        private readonly IGrpcEventQueue _grpcEventQueue;

        public PlatformGrpcService(
            AppDbContext appDbContext,
            PlatformAppService platformAppService,
            IGrpcEventQueue grpcEventQueue)
        {
            _appDbContext = appDbContext;
            _platformAppService = platformAppService;
            _grpcEventQueue = grpcEventQueue;
        }

        public override async Task<PlatformsGrpcResponse> GetPlatforms(GetPlatformsGrpcRequest request, ServerCallContext context)
        {
            var platforms = await _appDbContext.Platforms.ToListAsync(context.CancellationToken);

            context.Status = new Status(StatusCode.OK, detail: string.Empty);

            var response = new PlatformsGrpcResponse();
            response.Items.AddRange(platforms.Select(p => new PlatformItemGrpcResponse
            {
                Id = p.Id,
                Name = p.Name,
            }));
            return response;
        }

        public override async Task<CommandsGrpcResponse> GetCommands(GetCommandsGrpcRequest request, ServerCallContext context)
        {
            var commands = await _appDbContext.Commands.ToListAsync(context.CancellationToken);

            context.Status = new Status(StatusCode.OK, detail: string.Empty);

            var response = new CommandsGrpcResponse();
            response.Items.AddRange(commands.Select(c => new CommandItemGrpcResponse
            {
                Id = c.Id,
                CommandLine = c.CommandLine,
                Help = c.Help,
                PlatformId = c.PlatformId,
            }));
            return response;
        }

        public override async Task<AddPlatformGrpcResponse> AddPlatform(AddPlatformGrpcRequest request, ServerCallContext context)
        {
            var response = await _platformAppService.AddPlatform(new AddPlatformRequest
            {
                Name = request.Name,
            }, context.CancellationToken);

            context.Status = new Status(StatusCode.OK, detail: string.Empty);

            return new AddPlatformGrpcResponse
            {
                Id = response.Id,
                Name = response.Name,
            };
        }

        public override async Task SubscribeToAddPlatform(SubscribeToPlatformAddedGrpcStream request, IServerStreamWriter<PlatformAddedGrpcEvent> responseStream, ServerCallContext context)
        {
            var @event = await _grpcEventQueue.GetEvent(context.CancellationToken);

            if (@event is PlatformAddedEvent platformAddedEvent)
            {
                await responseStream.WriteAsync(new PlatformAddedGrpcEvent
                {
                    Id = platformAddedEvent.Id,
                    Name = platformAddedEvent.Name,
                }, context.CancellationToken);
            }
        }
    }
}
