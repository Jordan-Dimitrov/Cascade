using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Shared.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime OccuredOnUtc { get; set; }
        public DateTime? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
