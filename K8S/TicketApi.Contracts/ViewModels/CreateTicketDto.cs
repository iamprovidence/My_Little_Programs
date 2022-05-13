namespace TicketApi.Contracts.ViewModels
{
    public class CreateTicketDto
    {
        public TicketType Type { get; init; }
        public string Number { get; init; }
        public string Code { get; init; }
    }
}
