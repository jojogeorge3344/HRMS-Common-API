using Chef.Common.Core;
using SqlKata;
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
        Task<T> GetAsync(object whereConditionObject, CancellationToken cancellationToken = default);
        Task<int> InsertAsync(T obj);
        Task<int> UpdateAsync(T obj);
        Task<int> DeleteAsync(int id); 
        Task<IEnumerable<T>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(int noOfRecords, object whereConditionObject, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetRecordsAsync(object whereConditionObject, CancellationToken cancellationToken = default);
        Task<int> InsertAsync(object insertObject);
        Task<int> BulkInsertAsync(IEnumerable<object> bulkInsertObjects);
        Task<int> UpdateAsync(object updateObject, object updateConditionObject);
        Task<int> DeleteAsync(object deleteCondition);
        Task<int> DeleteAsync(Query sqlKataQuery);
    }
}