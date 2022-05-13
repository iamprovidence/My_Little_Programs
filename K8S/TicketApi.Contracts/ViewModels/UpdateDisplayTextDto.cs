using Common.Structs;

namespace TicketApi.Contracts.ViewModels
{
    public class UpdateDisplayTextDto
    {
        public int TicketId { get; init; }
        public LanguageCode LanguageCode { get; init; }
        public string DislayText { get; init; }
    }
}
