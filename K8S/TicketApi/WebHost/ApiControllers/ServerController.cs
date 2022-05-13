using Microsoft.AspNetCore.Mvc;

namespace TicketApi.WebHost.ApiControllers
{
    [Route("api/server")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        public string GetHealthStatus()
        {
            return "Running...";
        }
    }
}
