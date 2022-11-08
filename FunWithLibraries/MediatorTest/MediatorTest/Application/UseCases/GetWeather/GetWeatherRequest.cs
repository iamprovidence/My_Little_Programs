using CSharpFunctionalExtensions;
using Mediator;
using MediatorTest.Application.Common;

namespace MediatorTest.Application.UseCases.GetWeather
{
    public class GetWeatherRequest : IRequest<Result<IReadOnlyCollection<WeatherForecastDto>, ErrorObject>>
    {
        // 2022-11-07
        public DateOnly? Date { get; init; }
    }
}
