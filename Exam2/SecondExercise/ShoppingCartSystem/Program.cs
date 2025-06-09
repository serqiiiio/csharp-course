namespace ShoppingCartSystem;

public class Program
{
    static void Main()
    {
        var cart = new ShoppingCart();

        var laptop = new PhysicalProduct
        {
            Name = "Gaming Laptop",
            Price = 1200,
            Stock = 5,
            Weight = 2.5m
        };

        var ebook = new DigitalProduct
        {
            Name = "Programming Guide",
            Price = 29.99m,
            DownloadUrl = "http://example.com/download/book.pdf"
        };

        var mouse = new PhysicalProduct
        {
            Name = "Wireless Mouse",
            Price = 25,
            Stock = 10,
            Weight = 0.2m
        };

        cart.AddItem(laptop);
        cart.AddItem(ebook);
        cart.AddItem(mouse, 2);

        cart.DisplayCart();
        cart.Checkout();
    }
}