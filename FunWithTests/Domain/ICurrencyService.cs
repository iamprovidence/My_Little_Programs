using System.Threading.Tasks;

namespace Domain
{
    public interface ICurrencyService
    {
        Task<Money> Convert(Money originalAmount, Currency destinationCurrency);
    }
}