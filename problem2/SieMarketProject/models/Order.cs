namespace SieMarketProject.models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    // Calculate final price including 10% discount if total > 500
    public decimal GetFinalPrice()
    {
        decimal total = Items.Sum(i => i.Quantity * i.UnitPrice);
        
        if (total > 500)
        {
            total *= 0.9m; // Apply 10% discount
        }
        
        return total;
    }


}