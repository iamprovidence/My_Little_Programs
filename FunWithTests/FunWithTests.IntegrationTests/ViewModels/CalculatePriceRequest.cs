using Domain;

namespace FunWithTests.IntegrationTests.ViewModels
{
    public class CalculatePriceRequest
    {
        public string OrderType { get; internal init; }
        public Money Money { get; internal init; }
    }
}