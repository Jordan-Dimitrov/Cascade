using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
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
