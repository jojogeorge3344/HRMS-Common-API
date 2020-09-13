using Chef.Common.Core;
using System;
using System.Collections.Generic;
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
            this.queryBuilder = queryBuilder;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where("id", id).AsDelete();
            return await databaseSession.ExecuteAsync(query);
        }

        public async Task<int> InsertAsync(TModel obj)
        {
            InsertModelProperties(ref obj);
            return await databaseSession.ExecuteScalarAsync<int>(sql: queryBuilder.GenerateInsertQuery(), param: obj);
        }

        public async Task<int> UpdateAsync(TModel obj)
        {
            UpdateModelProperties(ref obj);
            return await databaseSession.ExecuteAsync(sql: queryBuilder.GenerateUpdateQuery(), param: obj);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>();
            return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);

        }
        public async Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = sqlQueryBuilder.Query<TModel>().Where(new { id });
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
        void InsertModelProperties(ref TModel obj)
        {
            obj.CreatedBy = obj.ModifiedBy = 1;
            obj.CreatedDate = obj.ModifiedDate = DateTime.UtcNow;
            obj.IsArchived = false;
        }
        void UpdateModelProperties(ref TModel obj)
        {
            obj.ModifiedBy = 1;
            obj.CreatedDate = obj.ModifiedDate = DateTime.UtcNow;
        }
    }
}