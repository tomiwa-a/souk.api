

namespace Souk.Domain.Inventory.ValueObjects;

public record Money
{
    public decimal Value { get; init; }

    private Money(decimal value)
    {
        this.Value = value;
    }

    public static Money Create(decimal value)
    {
        return value < 0 ? throw new ArgumentException("Money cannot be negative", nameof(value)) : new Money(decimal.Round(value, 2));
    }
    
    public static implicit operator Money(decimal value) => Create(value);
};