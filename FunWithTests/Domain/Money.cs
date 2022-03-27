using System;

namespace Domain
{
    public readonly struct Money : IEquatable<Money>
    {
        public decimal Amount { get; init; }
        public Currency Currency { get; init; }


        public Money(decimal amount, Currency currency)
        {
            (Amount, Currency) = (amount, currency);
        }

        public bool IsZero() => Amount == default;
        public static Money Zero => new(default, default);

        public override bool Equals(object? obj) => obj is Money o && Equals(o);

        public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Money left, Money right) => left.Equals(right);

        public static bool operator !=(Money left, Money right) => !(left == right);

        public static Money operator +(Money left, Money right) => left.Add(right);

        public static Money operator -(Money left, Money right) => left.Subtract(right);

        public Money Subtract(Money debit) => new(Math.Round(Amount - debit.Amount, 2), Currency);

        public Money Add(Money amount) => new(Math.Round(Amount + amount.Amount, 2), Currency);
    }
}