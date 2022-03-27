using System.Threading.Tasks;
using Domain;
using FunWithTests.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunWithTests.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/order")]
    public class OrderController
    {
        private readonly IOrderPriceCalculator _orderPriceCalculator;

        public OrderController(IOrderPriceCalculator orderPriceCalculator)
        {
            _orderPriceCalculator = orderPriceCalculator;
        }

        [HttpPost]
        [Route("calculate-price")]
        public Task<Money> CalculatePrice(CalculatePriceRequest request)
        {
            return _orderPriceCalculator.Calculate(OrderType.FromName(request.OrderType), request.Money);
        }
    }
}
