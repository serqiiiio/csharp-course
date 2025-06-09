namespace ShoppingCartSystem;

public class ShoppingCart
{
    private const string QuantityMustBePositiveMessage = "Quantity must be positive.";
    private const string AddedToCartMessage = "Added {0} x {1} to cart";
    private const string CheckoutHeader = "\n=== Checkout Process ===";
    private const string ShippedMessage = "- {0}: Shipped (Weight: {1})";
    private const string ShippingFailedMessage = "- {0}: Shipping failed - {1}";
    private const string DownloadedMessage = "- {0}: Downloaded";
    private const string DownloadFailedMessage = "- {0}: Download failed - {1}";
    private const string TotalAmountMessage = "\nTotal Amount: ${0:F2}";
    private const string PaymentProcessedMessage = "Payment processed successfully!";
    private const string OrderConfirmationMessage = "Order confirmation sent to customer email.";
    private const string CartHeader = "\n=== Shopping Cart ===";
    private const string CartItemMessage = "{0} x{1} - ${2:F2}";
    private const string CartTotalMessage = "Total: ${0:F2}";

    private readonly List<IProduct> items = new();
    private readonly IDiscountStrategy discountStrategy;
    private readonly IShippingCalculator shippingCalculator;

    public ShoppingCart(
        IDiscountStrategy? discountStrategy = null,
        IShippingCalculator? shippingCalculator = null)
    {
        this.discountStrategy = discountStrategy ?? new DefaultDiscountStrategy();
        this.shippingCalculator = shippingCalculator ?? new DefaultShippingCalculator();
    }

    public void AddItem(IProduct product, int quantity = 1)
    {
        if (quantity <= 0)
            throw new ArgumentException(QuantityMustBePositiveMessage, nameof(quantity));
        for (int i = 0; i < quantity; i++)
            items.Add(product);

        Console.WriteLine(string.Format(AddedToCartMessage, quantity, product.Name));
    }

    public decimal CalculateTotal()
    {
        decimal subtotal = items.Sum(item => item.Price);
        decimal shipping = shippingCalculator.CalculateShipping(items);
        decimal discount = discountStrategy.CalculateDiscount(items, subtotal);
        return subtotal + shipping - discount;
    }

    public void Checkout()
    {
        Console.WriteLine(CheckoutHeader);

        foreach (var item in items.OfType<IShippable>())
        {
            try
            {
                item.Ship();
                Console.WriteLine(string.Format(ShippedMessage, ((IProduct)item).Name, ((IProduct)item).Weight));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(ShippingFailedMessage, ((IProduct)item).Name, ex.Message));
            }
        }

        foreach (var item in items.OfType<IDownloadable>())
        {
            try
            {
                item.Download();
                Console.WriteLine(string.Format(DownloadedMessage, ((IProduct)item).Name));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(DownloadFailedMessage, ((IProduct)item).Name, ex.Message));
            }
        }

        Console.WriteLine(string.Format(TotalAmountMessage, CalculateTotal()));
        Console.WriteLine(PaymentProcessedMessage);
        Console.WriteLine(OrderConfirmationMessage);
    }

    public void DisplayCart()
    {
        Console.WriteLine(CartHeader);
        var grouped = items.GroupBy(p => p.Name);

        foreach (var group in grouped)
        {
            var product = group.First();
            var quantity = group.Count();
            Console.WriteLine(string.Format(CartItemMessage, product.Name, quantity, product.Price * quantity));
        }

        Console.WriteLine(string.Format(CartTotalMessage, CalculateTotal()));
    }
}
