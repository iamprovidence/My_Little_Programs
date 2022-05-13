using Microsoft.AspNetCore.Mvc;
using TicketApi.Application;
using TicketApi.Contracts.ViewModels;

namespace TicketApi.WebHost.ApiControllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketAppService _appService;

        public TicketController(TicketAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("{id}")]
        public Task<TicketItemDto> GetTicket(int id, CancellationToken cancellationToken)
        {
            return _appService.GetTicket(id, cancellationToken);
        }

        [HttpGet]
        public Task<IReadOnlyCollection<TicketListItemDto>> GetTickets(CancellationToken cancellationToken)
        {
            return _appService.GetTickets(cancellationToken);
        }

        [HttpPost]
        public Task<int> CreateTicket(CreateTicketDto createTicket, CancellationToken cancellationToken)
        {
            return _appService.CreateTicket(createTicket, cancellationToken);
        }
    }
}
