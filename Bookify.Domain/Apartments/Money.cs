namespace Bookify.Domain.Apartments;

public record Money(decimal Amount, Currency currency)
{
    public static Money operator +(Money left, Money right)
    {
        if (left.currency != right.currency)
            throw new InvalidOperationException("Cannot add Money with different currencies.");
        return new Money(left.Amount + right.Amount, left.currency);
    }

    public static Money Zero() => new Money(0, Currency.None);

    public static Money Zero(Currency currency)=> new Money(0, currency);
    public bool IsZero() => Amount == 0 && currency == Currency.None;

}