namespace Dotnet9.DomainCommons.Models;

public abstract record BaseEntity : IEntity, IDomainEvents
{
    [NotMapped] private readonly List<INotification> _domainEvents = new();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void AddDomainEventIfAbsent(INotification eventItem)
    {
        if (!_domainEvents.Contains(eventItem))
        {
            _domainEvents.Add(eventItem);
        }
    }

    public IEnumerable<INotification> GetDomainEvents()
    {
        return _domainEvents;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public abstract object[] GetKeys();

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {GetKeys().JoinAsString(",")}";
    }
}