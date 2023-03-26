namespace Dotnet9.WebAPI.Service.Domain.Aggregates.Orders;

public class OrderItem : Entity<int>
{
    public int ProductId { get; set; }

    public float Price { get; set; }
}