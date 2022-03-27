using System.Threading.Tasks;
using Domain;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FunWithTests.UnitTests
{
    public class NSubstituteOrderPriceCalculatorTests
    {
        [Fact]
        public async Task Order_price_should_be_calculated_for_regular_order()
        {
            // Arrange
            var money = new Money(200, Currency.EUR);

            var currencyService = Substitute.For<ICurrencyService>();

            currencyService
                .Convert(Arg.Any<Money>(), Arg.Any<Currency>())
                .Returns(new Money(0, Currency.EUR));

            var sut = new OrderPriceCalculator(currencyService);

            // Act
            var result = await sut.Calculate(OrderType.RegularOrder, money);

            // Assert
            result.Amount.Should().Be(200);
        }

        [Fact]
        public async Task Order_price_should_be_calculated_for_expensive_order()
        {
            // Arrange
            var money = new Money(200, Currency.EUR);

            var currencyService = Substitute.For<ICurrencyService>();

            currencyService
                .Convert(OrderType.ExpensiveOrder.GetDiscount(), Currency.EUR)
                .Returns(new Money(80, Currency.EUR));

            var sut = new OrderPriceCalculator(currencyService);

            // Act
            var result = await sut.Calculate(OrderType.ExpensiveOrder, money);

            // Assert
            await currencyService
                .Received(requiredNumberOfCalls: 1)
                .Convert(Arg.Any<Money>(), Arg.Is<Currency>(c => c == Currency.EUR));

            await currencyService
                .DidNotReceive()
                .Convert(Arg.Any<Money>(), Currency.USD);

            result.Amount.Should().Be(120);
        }
    }
}
