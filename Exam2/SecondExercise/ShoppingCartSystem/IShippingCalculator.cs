namespace ShoppingCartSystem;

public interface IShippingCalculator
{
    decimal CalculateShipping(IEnumerable<IProduct> items);
}