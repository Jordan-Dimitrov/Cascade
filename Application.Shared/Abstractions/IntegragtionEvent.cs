using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Abstractions
{
    public abstract record IntegragtionEvent 
    {
        protected IntegragtionEvent()
        {
            Id = Guid.NewGuid();
            OccuredOnUtc = DateTime.UtcNow;
        }
        public Guid Id { get; protected init; }
        public DateTime OccuredOnUtc { get; protected init; }
    }
}
