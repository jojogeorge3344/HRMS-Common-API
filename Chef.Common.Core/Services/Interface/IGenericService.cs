using System.Threading;
using Chef.Common.Core.Services;
using Chef.Common.Repositories;

namespace Chef.Common.Services;

public interface IGenericService<TModel> : IBaseService
{
    Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default);
    Task<int> InsertAsync(TModel obj);
    Task<int> UpdateAsync(TModel obj);
    Task<int> DeleteAsync(int id);
    Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default);
}
