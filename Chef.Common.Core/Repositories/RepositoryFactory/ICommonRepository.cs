using Chef.Common.Core;
using SqlKata;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public interface ICommonRepository<T> : IRepository
        where T : IModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>IEnumerable<T></returns>


        #region Get Async
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetAsync(object whereConditionObject, CancellationToken cancellationToken = default);
        Task<T> GetAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default);
        #endregion

        #region Get Records Async
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(int noOfRecords, object whereConditionObject, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetRecordsAsync(object whereConditionObject, CancellationToken cancellationToken = default);

        #endregion

        #region Get Record Count 
        Task<int> GetRecordCountAsync(object whereConditionObject, CancellationToken cancellationToken = default);
        Task<int> GetRecordCountAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default);

        #endregion

        #region Insert Async
        Task<int> InsertAsync(T obj);
        Task<int> InsertAsync(object insertObject);
        Task<int> BulkInsertAsync(IEnumerable<object> bulkInsertObjects);
        #endregion

        #region Update Async
        Task<int> UpdateAsync(object updateObject, object updateConditionObject);
        Task<int> UpdateAsync(T obj);
        Task<int> UpdateAsync(object updateObject, SqlKata.Query sqlKataQuery);
        #endregion

        #region Delete Async
        Task<int> DeleteAsync(object deleteCondition);
        Task<int> DeleteAsync(Query sqlKataQuery);
        Task<int> DeleteAsync(int id);
        #endregion
    }
}