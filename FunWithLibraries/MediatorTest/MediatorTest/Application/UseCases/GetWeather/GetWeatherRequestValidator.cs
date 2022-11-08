using FluentValidation;

namespace MediatorTest.Application.UseCases.GetWeather
{
    public class GetWeatherRequestValidator : AbstractValidator<GetWeatherRequest>
    {
        public GetWeatherRequestValidator()
        {
            RuleFor(v => v.Date)
                .NotNull();
        }
    }
}
