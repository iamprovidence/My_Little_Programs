using System.Threading.Tasks;

namespace Domain
{
    public class OrderPriceCalculator : IOrderPriceCalculator
    {
        private readonly ICurrencyService _currencyService;

        public OrderPriceCalculator(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<Money> Calculate(OrderType orderType, Money money)
        {
            var discount = orderType.GetDiscount();

            var discountInCorrectCurrency = await _currencyService.Convert(discount, money.Currency);

            return money - discountInCorrectCurrency;
        }
    }
}
