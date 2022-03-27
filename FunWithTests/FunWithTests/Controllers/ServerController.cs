using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunWithTests.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/server")]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Running...");
        }
    }
}
