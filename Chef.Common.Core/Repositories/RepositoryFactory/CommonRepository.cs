using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    internal partial class CommonRepository<TModel> : ICommonRepository<TModel> where TModel : Model
    {
        readonly IDatabaseSession databaseSession;

        readonly IQueryBuilder<TModel> queryBuilder;
        readonly ISqlQueryBuilder sqlQueryBuilder;
        //readonly UnitOfWork unitOfWork;
        public CommonRepository(IDatabaseSession databaseSession,
            ISqlQueryBuilder sqlQueryBuilder, IQueryBuilder<TModel> queryBuilder)
        {
            this.databaseSession = databaseSession;
            this.queryBuilder = queryBuilder;
            this.sqlQueryBuilder = sqlQueryBuilder;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where("id", id).AsDelete();
            return await databaseSession.ExecuteAsync(query);
        }
        public async Task<int> DeleteAsync(object deleteCondition)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(deleteCondition).AsDelete();
            return await databaseSession.ExecuteAsync(query);
        } 

        public async Task<int> InsertAsync(TModel obj)
        {
            InsertModelProperties(ref obj);
            var query = sqlQueryBuilder.Query<TModel>().AsInsertExt(obj, returnId: true);
            return await databaseSession.ExecuteScalarAsync<int>(query);
        }

        public async Task<int> UpdateAsync(TModel obj)
        {
            UpdateModelProperties(ref obj);
            var query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(obj).Where(new { id= obj.Id});
            return await databaseSession.ExecuteAsync(query);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);

        }

        public async Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            _ = query.ApplySqlSearch(sqlSearch);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken); 
        }

        public async Task<IEnumerable<TModel>> GetRecordsAsync(object whereConditionObject, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(whereConditionObject);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
        }

        public async Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(new { id });
            return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
        }
        public async Task<TModel> GetAsync(object whereConditionObject, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(whereConditionObject);
            return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
        }
        //public async Task<int> DeleteAsync(int id)
        //{
        //    var query = sqlQueryBuilder.Query<TModel>().Where(new { id }).AsDelete();
        //    return await databaseSession.ExecuteAsync(query);
        //}

        //public async Task<int> DeleteAsync(TModel obj)
        //{
        //    var query = sqlQueryBuilder.Query<TModel>().Where("id", obj.Id).AsDelete();
        //    return await databaseSession.ExecuteAsync(query);
        //}
        //public async Task<int> InsertAsync(TModel obj)
        //{
        //    InsertModelProperties(ref obj);
        //    var query = sqlQueryBuilder.Query<TModel>().AsInsert(obj.ToLowerReadOnlyDictionary(), returnId: true);
        //    sqlQueryBuilder.Query<TModel>().AsInsert()
        //    return await databaseSession.ExecuteScalarAsync<int>(query);
        //}

        //public async Task<int> UpdateAsync(TModel obj)
        //{
        //    UpdateModelProperties(ref obj);
        //    var query = sqlQueryBuilder.Query<TModel>().Where("id", obj.Id).AsUpdate(obj.ToLowerReadOnlyDictionary());
        //    return await databaseSession.ExecuteAsync(query);
        //}
        public async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().OrderByDesc("id").Limit(noOfRecords);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
        }
        public async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, object whereConditionObject, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(whereConditionObject).OrderByDesc("id").Limit(noOfRecords);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
        }
        void InsertModelProperties(ref TModel obj)
        {
            obj.CreatedBy = obj.ModifiedBy = "system";
            obj.CreatedDate = obj.ModifiedDate = DateTime.UtcNow;
            obj.IsArchived = false;
        }
        void UpdateModelProperties(ref TModel obj)
        {
            obj.ModifiedBy = "system";
            obj.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<int> InsertAsync(object insertObject)
        {
            //Created Date/By handled in extension
            //Modified Date/By handled in extension
            var query = sqlQueryBuilder.Query<TModel>().AsInsertExt(insertObject, returnId: true);
            return await databaseSession.ExecuteScalarAsync<int>(query);
        }

        public async Task<int> UpdateAsync(object updateObject, object updateConditionObject)
        {
            //Modified Date/By handled in extension
            var query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(updateObject).Where(updateConditionObject);
            return await databaseSession.ExecuteAsync(query);
        }

        public async Task<int> BulkInsertAsync(IEnumerable<object> bulkInsertObjects)
        {
            //Created Date/By handled in extension
            //Modified Date/By handled in extension
            var query = sqlQueryBuilder.Query<TModel>().AsBulkInsertExt(bulkInsertObjects);
            return await databaseSession.ExecuteAsync(query);
        }

        public async Task<int> DeleteAsync(SqlKata.Query sqlKataQuery)
        {
            var query = sqlKataQuery.AsDelete();
            return await databaseSession.ExecuteAsync(query);
        }
    }
}