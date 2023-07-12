using Chef.Common.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories;

/// <summary>
/// TODO: Depreciated. Eventually will be removed.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public abstract class CommonGenericRepository<TModel> : BaseRepository, ICommonGenericRepository<TModel>
    where TModel : Model
{
    public virtual async Task<int> DeleteAsync(int id)
      => await GenericRepository<TModel>().DeleteAsync(id);

    public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    => await GenericRepository<TModel>().GetAllAsync(cancellationToken);

    public virtual async Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default)
    => await GenericRepository<TModel>().GetAsync(id, cancellationToken);

    public virtual async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default)
    => await GenericRepository<TModel>().GetRecordsAsync(noOfRecords, cancellationToken);

    public virtual async Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default)
    => await GenericRepository<TModel>().GetRecordsAsync(sqlSearch, cancellationToken);

    public virtual async Task<int> InsertAsync(TModel obj)
    => await GenericRepository<TModel>().InsertAsync(obj);

    public virtual async Task<int> UpdateAsync(TModel obj)
    => await GenericRepository<TModel>().UpdateAsync(obj);
}
