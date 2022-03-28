using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain;
using FluentAssertions;
using Moq;
using Xunit;

namespace FunWithTests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class MoqOrderPriceCalculatorTests
    {
        [Fact]
        public async Task Order_price_should_be_calculated_for_regular_order()
        {
            // Arrange
            var money = new Money(200, Currency.EUR);

            var currencyService = new Mock<ICurrencyService>();

            currencyService
                .Setup(x => x.Convert(It.IsAny<Money>(), It.IsAny<Currency>()))
                .ReturnsAsync(new Money(0, Currency.EUR));

            var sut = new OrderPriceCalculator(currencyService.Object);

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

            var currencyService = new Mock<ICurrencyService>();

            currencyService
                .Setup(x => x.Convert(OrderType.ExpensiveOrder.GetDiscount(), Currency.EUR))
                .ReturnsAsync(new Money(80, Currency.EUR));

            var sut = new OrderPriceCalculator(currencyService.Object);

            // Act
            var result = await sut.Calculate(OrderType.ExpensiveOrder, money);

            // Assert
            currencyService
                .Verify(x => x.Convert(It.IsAny<Money>(), It.Is<Currency>(c => c == Currency.EUR)), Times.Once);

            currencyService
                .Verify(x => x.Convert(It.IsAny<Money>(), Currency.USD), Times.Never);

            result.Amount.Should().Be(120);
        }
    }
}
