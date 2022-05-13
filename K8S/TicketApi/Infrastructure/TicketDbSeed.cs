using TicketApi.Domain;

namespace TicketApi.Infrastructure
{
    public static class TicketDbSeed
    {
        public static void Seed(TicketDbContext dbContext)
        {
            if (!dbContext.Tickets.Any())
            {
                var ticket = TicketFactory.CreateNew(TicketType.Regular, "ABC", "1234");

                dbContext.Tickets.Add(ticket);

                dbContext.SaveChanges();
            }
        }
    }
}
