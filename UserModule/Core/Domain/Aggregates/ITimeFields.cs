using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface ITimeFields
    {
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
