
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public interface IGenericRepository<T> : IRepository
    {
        Task<int> DeleteAsync(int id);
        Task<int> ArchiveAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T obj);
        Task<int> UpdateAsync(T obj);
        Task<List<T>> BulkInsertAsync(List<T> objs);
        Task<int> InsertAuditAsync(object obj, int parentID, int auditId = 0);
    }
}