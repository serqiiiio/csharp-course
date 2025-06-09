namespace EmployeeManagementSystem;

public class EmployeeName
{
    public string Value { get; }
    public EmployeeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.");
        Value = value;
    }
    public override string ToString() => Value;
}

public class Money
{
    public decimal Value { get; }
    public Money(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Amount cannot be negative.");
        Value = value;
    }
    public override string ToString() => Value.ToString("F2");

    public static Money operator +(Money a, Money b) => new Money(a.Value + b.Value);
    public static Money operator *(Money money, decimal multiplier) => new Money(money.Value * multiplier);
    public static implicit operator decimal(Money m) => m.Value;
}