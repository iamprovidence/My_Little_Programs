using Common.Structs;
using DisplayTextWorker.Domain.Builders;
using DisplayTextWorker.Domain.Parser;
using TicketApi.Contracts;
using TicketApi.Contracts.ViewModels;

namespace DisplayTextWorker.Application
{
    public class DisplayTextAppService
    {
        private readonly ITicketApiClient _ticketApiClient;
        private readonly DisplayTextParser _parser;

        public DisplayTextAppService(ITicketApiClient ticketApiClient)
        {
            _ticketApiClient = ticketApiClient;
            _parser = new DisplayTextParser();
        }

        public async Task GenerateDisplayText(int ticketId, CancellationToken cancellationToken)
        {
            var ticketInfo = await _ticketApiClient.GetTicket(ticketId, cancellationToken);

            var tokens = _parser.Parse(new TicketParserInputDto
            {
                TicketType = ticketInfo.Type,
                DisplayText = ticketInfo.DisplayText,
                Code = ticketInfo.Code,
                Number = ticketInfo.Number,
            });

            foreach (var languageCode in Enum.GetValues<LanguageCode>())
            {
                var formattedDisplayText = BuilderFactory.GetBuilder(languageCode).Build(tokens);

                await _ticketApiClient.UpdateDisplayText(new UpdateDisplayTextDto
                {
                    TicketId = ticketId,
                    DislayText = formattedDisplayText,
                    LanguageCode = languageCode,
                }, cancellationToken);
            }
        }
    }
}
