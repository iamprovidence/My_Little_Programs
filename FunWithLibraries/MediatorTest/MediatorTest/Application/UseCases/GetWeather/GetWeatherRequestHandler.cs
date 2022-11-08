using CSharpFunctionalExtensions;
using Mediator;
using MediatorTest.Application.Common;

namespace MediatorTest.Application.UseCases.GetWeather
{
    internal class GetWeatherRequestHandler : IRequestHandler<GetWeatherRequest, Result<IReadOnlyCollection<WeatherForecastDto>, ErrorObject>>
    {
        public async ValueTask<Result<IReadOnlyCollection<WeatherForecastDto>, ErrorObject>> Handle(GetWeatherRequest request, CancellationToken cancellationToken)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var result = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecastDto
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)],
                })
                .ToList();

            return result;
        }
    }
}
