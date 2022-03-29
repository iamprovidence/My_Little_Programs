using System.Threading.Tasks;
using Domain;
using Moq;
using VerifyXunit;
using Xunit;

namespace FunWithTests.SnapshotTests
{
    [UsesVerify]
    public class OrderPriceCalculatorTests
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
            await Verifier.Verify(result);
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
            await Verifier.Verify(result);
        }
    }
}
