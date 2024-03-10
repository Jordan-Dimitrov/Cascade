namespace Application.Shared.Abstractions
{
    public interface IEventBus
    {
        Task PublisAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}
