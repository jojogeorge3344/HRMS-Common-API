using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Common.Repositories;

/// <summary>
/// TODO : Depreciated. Eventually will be removed.
/// </summary>
/// <typeparam name="TModel"></typeparam>
internal partial class CommonRepository<TModel> : ICommonRepository<TModel> where TModel : IModel
{
    private readonly IDatabaseSession databaseSession;
    private readonly IQueryBuilder<TModel> queryBuilder;
    private readonly ISqlQueryBuilder sqlQueryBuilder;
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
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().Where("id", id).AsDelete();
        return await databaseSession.ExecuteAsync(query);
    }

    #region Get Async
    public async Task<TModel> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate").Where(new { id });
        return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
    }

    public async Task<TModel> GetAsync(object whereConditionObject, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate").Where(whereConditionObject);
        return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
    }

    public async Task<TModel> GetAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate");
        query.ApplySqlSearch(sqlSearch);
        return await databaseSession.QueryFirstOrDefaultAsync<TModel>(query, cancellationToken: cancellationToken);
    }
    #endregion

    #region Get Records Async
    public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate");
        return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);

    }

    public async Task<IEnumerable<TModel>> GetRecordsAsync(SqlSearch sqlSearch = null, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate");
        query.ApplySqlSearch(sqlSearch);
        return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TModel>> GetRecordsAsync(object whereConditionObject, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate").Where(whereConditionObject);
        return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
    }


    public async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().OrderByDesc("createddate").Limit(noOfRecords);
        return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TModel>> GetRecordsAsync(int noOfRecords, object whereConditionObject, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().Where(whereConditionObject).OrderByDesc("createddate").Limit(noOfRecords);
        return await databaseSession.QueryAsync<TModel>(query, cancellationToken: cancellationToken);
    }


    #endregion

    #region Get Record Count
    public async Task<int> GetRecordCountAsync(object whereConditionObject, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsCount().OrderByDesc("createddate").Where(whereConditionObject);
        return await databaseSession.QueryFirstOrDefaultAsync<int>(query);
    }

    public async Task<int> GetRecordCountAsync(SqlSearch sqlSearch, CancellationToken cancellationToken = default)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsCount().OrderByDesc("createddate");
        query.ApplySqlSearch(sqlSearch);
        return await databaseSession.QueryFirstOrDefaultAsync<int>(query, cancellationToken: cancellationToken);
    }
    #endregion

    #region Insert Async

    public async Task<int> InsertAsync(TModel insertObject)
    {
        IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(insertObject);
        InsertModelProperties(ref expando);
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsInsertExt(expando, returnId: true);
        return await databaseSession.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> InsertAsync(object insertObject)
    {
        IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(insertObject);
        InsertModelProperties(ref expando);
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsInsertExt(expando, returnId: true);
        return await databaseSession.ExecuteScalarAsync<int>(query);
    }

    public async Task<int> BulkInsertAsync(IEnumerable<object> bulkInsertObjects)
    {
        List<IDictionary<string, object>> dictionaries = new();

        foreach (object record in bulkInsertObjects)
        {
            IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(record);
            InsertModelProperties(ref expando);
            dictionaries.Add(expando);
        }

        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsBulkInsertExt(dictionaries);

        return await databaseSession.ExecuteAsync(query);
    }

    #endregion

    #region Update Async

    public async Task<int> UpdateAsync(TModel obj)
    {
        IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(obj);
        UpdateModelProperties(ref expando);
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(expando).Where(new { id = obj.Id });
        return await databaseSession.ExecuteAsync(query);
    }

    public async Task<int> UpdateAsync(object updateObject, object updateConditionObject)
    {
        IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(updateObject);
        UpdateModelProperties(ref expando);
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().AsUpdateExt(expando).Where(updateConditionObject);
        return await databaseSession.ExecuteAsync(query);
    }

    public async Task<int> UpdateAsync(object updateObject, SqlKata.Query sqlKataQuery)
    {
        IDictionary<string, object> expando = sqlQueryBuilder.ToDictionary(updateObject);
        UpdateModelProperties(ref expando);
        SqlKata.Query query = sqlKataQuery.AsUpdateExt(expando);
        return await databaseSession.ExecuteAsync(query);
    }

    #endregion

    #region Delete Async 
    public async Task<int> DeleteAsync(object deleteCondition)
    {
        SqlKata.Query query = sqlQueryBuilder.Query<TModel>().Where(deleteCondition).AsDelete();
        return await databaseSession.ExecuteAsync(query);
    }

    public async Task<int> DeleteAsync(SqlKata.Query sqlKataQuery)
    {
        SqlKata.Query query = sqlKataQuery.AsDelete();
        return await databaseSession.ExecuteAsync(query);
    }

    #endregion

    private void UpdateModelProperties(ref IDictionary<string, object> expando)
    {
        if (expando.ContainsKey("createdBy"))
        {
            expando.Remove("createdBy");
        }

        if (expando.ContainsKey("createddate"))
        {
            expando.Remove("createddate");
        }

        expando["modifiedby"] = databaseSession.UserToken != null ? databaseSession.UserToken.UserName : "system";
        expando["modifieddate"] = DateTime.UtcNow;

        if (typeof(TModel).GetInterfaces().Contains(typeof(IBranchModel)))
        {
            expando["branchcode"] = databaseSession.UserToken?.BranchCode;
        }
    }

    private void InsertModelProperties(ref IDictionary<string, object> expando)
    {

        if (expando.ContainsKey("id"))
        {
            expando.Remove("id");
        }

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