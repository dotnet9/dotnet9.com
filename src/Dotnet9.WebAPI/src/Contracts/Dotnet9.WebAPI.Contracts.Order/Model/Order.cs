namespace Dotnet9.WebAPI.Contracts.Order.Model;

public class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime CreationTime { get; set; }
}