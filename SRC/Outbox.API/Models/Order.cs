
namespace Outbox.API.Models;

public sealed class Order
{
    public Order()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public required string TrackingNumber { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public string CustomerEmail { get; set; }
    public decimal TotalPrice { get; set; }
}
