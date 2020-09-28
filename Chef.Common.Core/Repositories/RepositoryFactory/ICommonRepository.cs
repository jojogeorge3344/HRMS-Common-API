using Chef.Common.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public interface ICommonRepository<T> : IRepository
        where T : Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>IEnumerable<T></returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default);
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetAsync(object constraints, CancellationToken cancellationToken = default);
        Task<int> InsertAsync(T obj);
        Task<int> UpdateAsync(T obj);
        Task<int> DeleteAsync(int id);

        Task<int> DeleteAsync(object constraints);
        Task<IEnumerable<T>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(object constraints, CancellationToken cancellationToken = default);
    }
}