namespace Bookify.Domain;


public abstract class Entity {
    public Guid Id {get; init;}
    private readonly List<IDomainEvent> domainEvents = new();

    public Entity(Guid id)
    {
        Id = id;
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() =>
        domainEvents.ToList();

    public void ClearDomainEvents() =>
        domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent) =>
        domainEvents.Add(domainEvent);
}