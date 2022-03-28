using System;
using System.Threading.Tasks;
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

        [HttpGet("time")]
        public async Task<DateTimeOffset> GetServerTime()
        {
            await Task.Delay(1000);

            return DateTimeOffset.Now;
        }
    }
}
