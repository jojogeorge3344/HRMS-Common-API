using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    /// <summary>
    /// Depreciated. Use only Generic Repository
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ICommonGenericRepository<TModel>
    {
        Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default);
        Task<int> InsertAsync(TModel obj);
        Task<int> UpdateAsync(TModel obj);
        Task<int> DeleteAsync(int id);
    }
}
