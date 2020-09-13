using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public interface IGenericRepository<T> : IRepository
    {
        Task<int> DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> InsertAsync(T obj);

        Task<int> UpdateAsync(T obj);
    }
}