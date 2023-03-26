namespace Dotnet9.WebAPI.Service.Domain.Aggregates.Orders;

public class Order : AggregateRoot<int>
{
    public string OrderNumber { get; private set; } = string.Empty;

    public AddressValue Address { get; private set; } = new();

    public List<OrderItem> Items { get; private set; }

    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;

    public Order(int id, string orderNumber) : this(id, orderNumber, new())
    {
    }

    public Order(int id, string orderNumber, AddressValue address)
    {
        Id = id;
        OrderNumber = orderNumber;
        Address = address;
        Items = new List<OrderItem>();
    }
}
