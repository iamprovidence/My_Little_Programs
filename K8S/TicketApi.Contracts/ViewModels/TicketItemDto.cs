using Common.Structs;

namespace TicketApi.Contracts.ViewModels
{
    public class TicketItemDto
    {
        public int Id { get; init; }
        public TicketType Type { get; init; }
        public string Number { get; init; }
        public string Code { get; init; }

        public string DisplayText { get; init; }
        public MultilingualString FormattedDisplayText { get; init; }
    }
}
