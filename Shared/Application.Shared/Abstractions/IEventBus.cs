using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Abstractions
{
    public interface IEventBus
    {
        Task PublisAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}
