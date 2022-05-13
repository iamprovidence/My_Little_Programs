using Common.Structs;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TicketApi.Application;
using TicketApi.Contracts.ViewModels;

namespace TicketApi.WebHost.GrpcEndpoints
{
    public class TicketGrpcEndpoint : TicketGrpc.TicketGrpcBase
    {
        private readonly TicketAppService _ticketAppService;

        public TicketGrpcEndpoint(TicketAppService ticketAppService)
        {
            _ticketAppService = ticketAppService;
        }

        public override async Task<Empty> UpdateDisplayText(UpdateDisplayTextGrpcDto request, ServerCallContext context)
        {
            var command = new UpdateDisplayTextDto
            {
                TicketId = request.TicketId,
                LanguageCode = (LanguageCode)request.LanguageCode,
                DislayText = request.DisplayText,
            };

            await _ticketAppService.UpdateDisplayText(command, context.CancellationToken);

            return new Empty();
        }
    }
}
