using Domain;

namespace FunWithTests.Models
{
    public class CalculatePriceRequest
    {
        public string OrderType { get; init; }
        public Money Money { get; init; }
    }
}