namespace Dotnet9.WebAPI.Service.Domain.Events;

public record OrderQueryDomainEvent : DomainEvent
{
    public List<Order> Orders { get; set; } = new();
}