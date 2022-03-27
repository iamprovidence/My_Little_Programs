using System;
using System.Threading.Tasks;
using Domain;
using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;

namespace FunWithTests.BehaviorTests.Steps
{
    [Binding]
    public class OrderPriceCalculatorSteps
    {
        private Money _orderPrice;
        private OrderType _orderType;
        private Mock<ICurrencyService> _currencyServiceMock;

        public OrderPriceCalculatorSteps()
        {
            _currencyServiceMock = new Mock<ICurrencyService>();
        }

        [Given(@"order with price of (.*) (.*)")]
        public void GivenOrderWithPrice(decimal price, string currency)
        {
            _orderPrice = new Money(price, Enum.Parse<Currency>(currency));
        }

        [Given(@"(.*) (.*) converts to (.*) (.*)")]
        public void GivenCurrencyConvertsToCurrency(decimal amount1, Currency currency1, decimal amount2, Currency currency2)
        {
            _currencyServiceMock
                .Setup(s => s.Convert(new Money(amount1, currency1), currency2))
                .ReturnsAsync(new Money(amount2, currency2));
        }

        [When(@"order is (.*) type")]
        public void WhenOrderIsType(string orderType)
        {
            _orderType = OrderType.FromName(orderType);

        }

        [Then(@"price should be (.*) (.*)")]
        public async Task ThenPriceShouldBe(decimal amount, string currency)
        {
            var priceCalculator = new OrderPriceCalculator(_currencyServiceMock.Object);

            var result = await priceCalculator.Calculate(_orderType, _orderPrice);

            result.Should().Be(new Money(amount, Enum.Parse<Currency>(currency)));
        }
    }
}
