using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Abstractions
{
    public record Link(string Href, string Rel, string Method);
}