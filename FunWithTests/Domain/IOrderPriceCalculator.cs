using System.Threading.Tasks;

namespace Domain
{
    public interface IOrderPriceCalculator
    {
        Task<Money> Calculate(OrderType orderType, Money money);
    }
}