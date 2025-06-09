namespace ShoppingCartSystem;

public class DefaultDiscountStrategy : IDiscountStrategy
{
    private const decimal LargeOrderThreshold = 100m;
    private const decimal LargeOrderDiscountRate = 0.1m;
    private const int BulkItemCount = 5;
    private const decimal BulkDiscount = 5m;

    public decimal CalculateDiscount(IEnumerable<IProduct> items, decimal subtotal)
    {
        decimal discount = 0;
        if (subtotal > LargeOrderThreshold)
            discount += subtotal * LargeOrderDiscountRate;
        if (items.Count() > BulkItemCount)
            discount += BulkDiscount;
        return discount;
    }
}