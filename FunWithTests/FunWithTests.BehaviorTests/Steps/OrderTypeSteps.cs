using Domain;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FunWithTests.BehaviorTests.Steps
{
    [Binding]
    public class OrderTypeSteps
    {
        [Given(@"Order types should have correct discount")]
        public void GivenOrderTypesShouldHaveCorrectDiscount(Table table)
        {
            var dataRows = table.CreateSet<(string, decimal, Currency)>();

            foreach (var dataRow in dataRows)
            {
                var orderType = OrderType.FromName(dataRow.Item1);
                var expectedDiscount = new Money(dataRow.Item2, dataRow.Item3);

                orderType.GetDiscount().Should().Be(expectedDiscount);
            }
        }
    }
}
