namespace ShoppingCartSystem;

public class DefaultShippingCalculator : IShippingCalculator
{
    public decimal CalculateShipping(IEnumerable<IProduct> items)
    {
        return items.OfType<IShippable>().Sum(item => ((IShippable)item).CalculateShippingCost());
    }
}