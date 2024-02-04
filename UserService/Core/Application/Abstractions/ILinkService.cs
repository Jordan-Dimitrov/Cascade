using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ILinkService
    {
        Link Generate(string endpointName, object? routeValues, string rel, string method);
    }
}
