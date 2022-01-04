using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAppTests.Controllers
{
	[ApiController]
	[Route("server")]
	public class ServerController : ControllerBase
	{
		[HttpGet("time")]
		public async Task<DateTimeOffset> GetServerTime()
		{
			await Task.Delay(1000);

			return DateTimeOffset.Now;
		}
	}
}
