using Domain.Shared.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Shared.Services
{
    public sealed class LinkService : ILinkService
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
