namespace Chef.Common.Core.Services
{
    public interface IAsyncService<T> : IBaseService
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> InsertAsync(T obj);
        Task<int> UpdateAsync(T obj);
        Task<int> DeleteAsync(int id);
    }
}