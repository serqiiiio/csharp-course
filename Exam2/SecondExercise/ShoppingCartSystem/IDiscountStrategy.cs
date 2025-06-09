namespace ShoppingCartSystem;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(IEnumerable<IProduct> items, decimal subtotal);
}