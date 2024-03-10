namespace Domain.Shared.Primitives
{
    public interface ICommandRepository<T> where T : class
    {
        Task InsertAsync(T value);

        Task UpdateAsync(T value);

        Task DeleteAsync(T value);
    }
}
