using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class LinkService : ILinkService
    {
        private readonly LinkGenerator _LinkGenerator;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _LinkGenerator = linkGenerator;
            _HttpContextAccessor = httpContextAccessor;
        }

        public Link Generate(string endpointName, object? routeValues, string rel, string method)
        {
            string str = _LinkGenerator.GetUriByName(_HttpContextAccessor.HttpContext, endpointName, routeValues);

            return new Link(_LinkGenerator.GetUriByName(
                _HttpContextAccessor.HttpContext,
                endpointName,
                routeValues),
                rel,
                method);
        }
    }
}
