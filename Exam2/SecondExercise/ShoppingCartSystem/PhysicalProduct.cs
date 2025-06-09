namespace ShoppingCartSystem;

public class PhysicalProduct : IProduct, IShippable
{
    private const decimal BaseShippingRate = 5.0m;
    private const decimal WeightShippingRate = 2.0m;
    private const string OutOfStockMessage = "Out of stock!";

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsPhysical => true;
    public decimal Weight { get; set; }

    public void Ship()
    {
        if (Stock <= 0)
            throw new InvalidOperationException(OutOfStockMessage);
        Stock--;
    }

    public decimal CalculateShippingCost()
    {
        return BaseShippingRate + (Weight * WeightShippingRate);
    }
}