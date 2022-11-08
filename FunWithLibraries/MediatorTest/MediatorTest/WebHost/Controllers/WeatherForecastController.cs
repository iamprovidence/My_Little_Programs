using Mediator;
using MediatorTest.Application.Common;
using MediatorTest.Application.UseCases.GetWeather;
using Microsoft.AspNetCore.Mvc;

namespace MediatorTest.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(typeof(IReadOnlyCollection<WeatherForecastDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] GetWeatherRequest request)
        {
            // can not return Result so here we are with inconsistent behaviour
            var result = await _mediator.Send(request, HttpContext.RequestAborted);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost(Name = "UpdateWeatherForecast")]
        [ProducesResponseType(typeof(IReadOnlyCollection<WeatherForecastDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromQuery] UpdateWeatherRequest request)
        {
            // can not return Result so here we are with inconsistent behaviour
            var result = await _mediator.Send(request, HttpContext.RequestAborted);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}