namespace ShoppingCartSystem;

public interface IShippable
{
    void Ship();
    decimal CalculateShippingCost();
}