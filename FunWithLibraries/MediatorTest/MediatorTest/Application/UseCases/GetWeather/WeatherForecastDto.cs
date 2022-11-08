namespace MediatorTest.Application.UseCases.GetWeather
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; init; }

        public int TemperatureC { get; init; }

        public string? Summary { get; init; }
    }
}
