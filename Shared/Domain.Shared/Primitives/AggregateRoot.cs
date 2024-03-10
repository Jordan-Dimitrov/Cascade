namespace Domain.Shared.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _DomainEvents = new List<IDomainEvent>();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _DomainEvents.Add(domainEvent);
        }
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _DomainEvents.ToList();
        public void ClearDomainEvents() => _DomainEvents.Clear();
    }
}
