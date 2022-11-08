using CSharpFunctionalExtensions;
using Mediator;
using MediatorTest.Application.Common;

namespace MediatorTest.Application.UseCases.GetWeather
{
    // by default, has a warning about missing handler
    public class UpdateWeatherRequest : IRequest<Result<IReadOnlyCollection<WeatherForecastDto>, ErrorObject>>
    {
    }
}
