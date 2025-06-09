namespace ShoppingCartSystem;

public interface IProduct
{
    string Name { get; }
    decimal Price { get; }
    int Stock { get; }
    bool IsPhysical { get; }
    decimal Weight { get; }
}