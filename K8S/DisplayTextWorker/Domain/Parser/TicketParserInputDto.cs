using TicketApi.Contracts.ViewModels;

namespace DisplayTextWorker.Domain.Parser
{
    public class TicketParserInputDto
    {
        public TicketType TicketType { get; init; }
        public string DisplayText { get; init; }
        public string Code { get; init; }
        public string Number { get; init; }
    }
}
