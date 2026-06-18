namespace Mtsorella.Api.Domain;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
