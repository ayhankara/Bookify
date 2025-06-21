namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();


    protected Entity(Guid id)
    {
        Id = id;
    }
    protected Entity()
    {

    }
    public Guid Id { get; private set; } = Guid.NewGuid();

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }
        _domainEvents.Add(domainEvent);
    }
}