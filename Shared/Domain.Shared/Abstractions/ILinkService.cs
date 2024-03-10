namespace Domain.Shared.Abstractions
{
    public interface ILinkService
    {
        Link Generate(string endpointName, object? routeValues, string rel, string method);
    }
}
