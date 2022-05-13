using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using TicketApi.Contracts.IntegrationEvents;
using TicketApi.Contracts.ViewModels;
using TicketApi.Domain;
using TicketApi.Infrastructure;

namespace TicketApi.Application
{
    public class TicketAppService
    {
        private static SemaphoreSlim Lock = new SemaphoreSlim(1, 1);

        private readonly IEventBus _eventBus;
        private readonly TicketDbContext _dbContext;

        public TicketAppService(
            IEventBus eventBus,
            TicketDbContext dbContext)
        {
            _eventBus = eventBus;
            _dbContext = dbContext;
        }

        public async Task<TicketItemDto> GetTicket(int id, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Tickets
                .Where(t => t.Id == id)
                .Select(t => new TicketItemDto
                {
                    Id = t.Id,
                    Type = t.Type.ToApiEnum(),
                    Number = t.Number,
                    Code = t.Code,
                    DisplayText = t.DisplayText,
                    FormattedDisplayText = t.FormattedDisplayText,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TicketListItemDto>> GetTickets(CancellationToken cancellationToken)
        {
            return await _dbContext
                .Tickets
                .Select(t => new TicketListItemDto
                {
                    Id = t.Id,
                    DisplayText = t.DisplayText,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateTicket(CreateTicketDto createTicketDto, CancellationToken cancellationToken)
        {
            var ticket = TicketFactory.CreateNew(createTicketDto.Type.ToDomainEnum(), createTicketDto.Number, createTicketDto.Code);

            _dbContext.Tickets.Add(ticket);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _eventBus.Publish(new TicketCreatedIntegrationEvent
            {
                TicketId = ticket.Id,
            });

            return ticket.Id;
        }

        public async Task UpdateDisplayText(UpdateDisplayTextDto updateDisplayTextDto, CancellationToken cancellationToken)
        {
            await Lock.WaitAsync(cancellationToken);

            var ticket = await _dbContext
                .Tickets
                .Where(x => x.Id == updateDisplayTextDto.TicketId)
                .SingleAsync(cancellationToken);

            ticket.UpdateDisplayText(updateDisplayTextDto.LanguageCode, updateDisplayTextDto.DislayText);

            await _dbContext.SaveChangesAsync(cancellationToken);

            Lock.Release();
        }
    }
}
