using System.ComponentModel;

namespace TicketApi.Domain
{
    public static class TicketFactory
    {
        public static Ticket CreateNew(TicketType type, string number, string code)
        {
            var displayText = GenerateDisplayText(type, number, code);

            return new Ticket(type, number, code, displayText);
        }

        private static string GenerateDisplayText(TicketType type, string number, string code)
        {
            return type switch
            {
                _ when type == TicketType.Regular => $"{code}{number}",
                _ when type == TicketType.Exclusive => $"{number}",
                _ => throw new InvalidEnumArgumentException($"Wrong enum value for ticket type {type}"),
            };
        }
    }
}
