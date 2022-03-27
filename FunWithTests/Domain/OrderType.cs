using Ardalis.SmartEnum;

namespace Domain
{
    public abstract class OrderType : SmartEnum<OrderType>
    {
        public OrderType(string name, int value)
            : base(name, value) { }

        public static readonly OrderType RegularOrder = new RegularOrder();
        public static readonly OrderType ExpensiveOrder = new ExpensiveOrder();

        public abstract Money GetDiscount();
    }

    public class RegularOrder : OrderType
    {
        public RegularOrder()
            : base(nameof(RegularOrder), 1) { }

        public override Money GetDiscount()
        {
            return Money.Zero;
        }
    }
    public class ExpensiveOrder : OrderType
    {
        public ExpensiveOrder()
            : base(nameof(ExpensiveOrder), 2) { }

        public override Money GetDiscount()
        {
            return new Money
            {
                Amount = 100,
                Currency = Currency.USD,
            };
        }
    }
}