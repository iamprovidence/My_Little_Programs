using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using FluentAssertions;
using FunWithTests.IntegrationTests.Helpers;
using FunWithTests.IntegrationTests.ViewModels;

namespace FunWithTests.IntegrationTests
{
    public class OrderApiTests
    {
        [FactSwitch]
        public async Task Post_CalculatePrice_ShouldSucceed()
        {
            // Arrange
            using var client = new FunWithTestsHttpClient();
            await client.Authenticate();

            // Act
            var result = await client.PostAsync("/api/order/calculate-price", new CalculatePriceRequest
            {
                OrderType = "RegularOrder",
                Money = new Money(100, Currency.USD),
            });

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await result.Content.ReadAsAsync<Money>();
            response.Amount.Should().Be(100);
        }
    }
}
