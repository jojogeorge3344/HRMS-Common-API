using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chef.Common.Core;
using Chef.Common.Repositories;
using Chef.Common.Services;

namespace Chef.Common.Services;

public abstract class GenericService<TModel> : BaseService, IGenericService<TModel>
    where TModel : Model
{
    public ICommonGenericRepository<TModel> GenericRepository { get; set; }

    public virtual async Task<int> DeleteAsync(int id)
    {
        return await GenericRepository.DeleteAsync(id);
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GenericRepository.GetAllAsync(cancellationToken);
    }

    public virtual async Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GenericRepository.GetAsync(id, cancellationToken);
    }

    public virtual async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default)
    {
        return await GenericRepository.GetRecordsAsync(noOfRecords, cancellationToken);
    }

    public virtual async Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default)
    {
        return await GenericRepository.GetRecordsAsync(sqlSearch, cancellationToken);
    }

    public virtual async Task<int> InsertAsync(TModel obj)
    {
        return await GenericRepository.InsertAsync(obj);
    }

    public virtual async Task<int> UpdateAsync(TModel obj)
    {
        return await GenericRepository.UpdateAsync(obj);
    }
}
