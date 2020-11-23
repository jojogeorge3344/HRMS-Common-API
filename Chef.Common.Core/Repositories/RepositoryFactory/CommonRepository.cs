using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    internal partial class CommonRepository<TModel> : ICommonRepository<TModel> where TModel : IModel
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

        #region Get Async
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
        public async Task<TModel> GetAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            query.ApplySqlSearch(sqlSearch); 
            return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
        }
        #endregion

        #region Get Records Async
        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);

        }

        public async Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            query.ApplySqlSearch(sqlSearch);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TModel>> GetRecordsAsync(object whereConditionObject, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(whereConditionObject);
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
        }
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


        #endregion

        #region Get Record Count
        public async Task<int> GetRecordCountAsync(object whereConditionObject, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().AsCount().Where(whereConditionObject);
            return await databaseSession.QueryFirstOrDefaultAsync<int>(query);
        }
        public async Task<int> GetRecordCountAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().AsCount();
            query.ApplySqlSearch(sqlSearch);
            return await databaseSession.QueryFirstOrDefaultAsync<int>(query, cancellationToken: cancellationToken);
        }
        #endregion

        #region Insert Async

        public async Task<int> InsertAsync(TModel insertObject)
        {
            IDictionary<string, object> expando = insertObject.ToDictionary();
            InsertModelProperties(ref expando);
            var query = sqlQueryBuilder.Query<TModel>().AsInsertExt(expando, returnId: true);
            return await databaseSession.ExecuteScalarAsync<int>(query);
        }
        public async Task<int> InsertAsync(object insertObject)
        {
            IDictionary<string, object> expando = insertObject.ToDictionary();
            InsertModelProperties(ref expando);
            var query = sqlQueryBuilder.Query<TModel>().AsInsertExt(expando, returnId: true);
            return await databaseSession.ExecuteScalarAsync<int>(query);
        }
        public async Task<int> BulkInsertAsync(IEnumerable<object> bulkInsertObjects)
        { 
            List<IDictionary<string, object>> dictionaries = new List<IDictionary<string, object>>();
            foreach (object record in bulkInsertObjects)
            {
                IDictionary<string, object> expando = record.ToDictionary();
                InsertModelProperties(ref expando);
                dictionaries.Add(expando);
            }
            var query = sqlQueryBuilder.Query<TModel>().AsBulkInsertExt(dictionaries);
            return await databaseSession.ExecuteAsync(query);
        }

        #endregion

        #region Update Async

        public async Task<int> UpdateAsync(TModel obj)
        {
            IDictionary<string, object> expando = obj.ToDictionary(); 
            UpdateModelProperties(ref expando);
            var query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(expando).Where(new { id = obj.Id });
            return await databaseSession.ExecuteAsync(query);
        }
        public async Task<int> UpdateAsync(object updateObject, object updateConditionObject)
        {
            IDictionary<string, object> expando = updateObject.ToDictionary();
            UpdateModelProperties(ref expando);
            var query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(expando).Where(updateConditionObject);
            return await databaseSession.ExecuteAsync(query);
        }
        public async Task<int> UpdateAsync(SqlKata.Query sqlKataQuery, object updateObject)
        {
            IDictionary<string, object> expando = updateObject.ToDictionary();
            UpdateModelProperties(ref expando);
            var query = sqlKataQuery.AsUpdateExt(expando);
            return await databaseSession.ExecuteAsync(query);
        }

        #endregion

        #region Delete Async 
        public async Task<int> DeleteAsync(object deleteCondition)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(deleteCondition).AsDelete();
            return await databaseSession.ExecuteAsync(query);
        }
        public async Task<int> DeleteAsync(SqlKata.Query sqlKataQuery)
        {
            var query = sqlKataQuery.AsDelete();
            return await databaseSession.ExecuteAsync(query);
        }

        #endregion



        void UpdateModelProperties(ref IDictionary<string, object> expando)
        {  
            if (expando.ContainsKey("createdBy"))
                expando.Remove("createdBy");
            if (expando.ContainsKey("createddate"))
                expando.Remove("createddate");
            expando["modifiedby"] = databaseSession.UserToken != null ? databaseSession.UserToken.UserName : "system";
            expando["modifieddate"] = DateTime.UtcNow;

            if (typeof(TModel).GetInterfaces().Contains(typeof(IBranchModel)))
            {
                expando["branchcode"] = databaseSession.UserToken?.BranchCode;
            }
        }
        void InsertModelProperties(ref IDictionary<string, object> expando)
        {
             
            if (expando.ContainsKey("id"))
                expando.Remove("id");
            expando["createdby"] = databaseSession.UserToken != null ? databaseSession.UserToken.UserName : "system";
            expando["modifiedby"] = databaseSession.UserToken != null ? databaseSession.UserToken.UserName : "system";
            expando["createddate"] = DateTime.UtcNow;
            expando["modifieddate"] = DateTime.UtcNow;
            expando["isarchived"] = false;

            if (typeof(TModel).GetInterfaces().Contains(typeof(IBranchModel)))
            { 
                expando["branchcode"] = databaseSession.UserToken?.BranchCode;
            }

        }




    }
}