namespace ShoppingCartSystem;

public class DigitalProduct : IProduct, IDownloadable
{
    private const int UnlimitedStock = int.MaxValue;
    private const decimal DigitalProductWeight = 0m;

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; } = UnlimitedStock;
    public bool IsPhysical => false;
    public decimal Weight => DigitalProductWeight;
    public string DownloadUrl { get; set; }

    public void Download()
    {
        Console.WriteLine($"Downloading {Name} from {DownloadUrl}");
    }
}