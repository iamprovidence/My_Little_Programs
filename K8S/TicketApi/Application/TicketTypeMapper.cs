namespace TicketApi.Application
{
    public static class TicketTypeMapper
    {
        public static Contracts.ViewModels.TicketType ToApiEnum(this Domain.TicketType ticketType)
        {
            return Enum.Parse<Contracts.ViewModels.TicketType>(ticketType.ToString());
        }

        public static Domain.TicketType ToDomainEnum(this Contracts.ViewModels.TicketType ticketType)
        {
            return Domain.TicketType.FromValue((int)ticketType);
        }
    }
}
