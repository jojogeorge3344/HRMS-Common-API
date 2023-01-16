using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Chef.Common.Core.Extensions;
using Chef.Common.Core.Logging;
using Chef.Common.Repositories;
using Dapper;
using Microsoft.AspNetCore.Http;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Chef.Common.Core.Repositories;

/// <summary>
/// TODO: Depreciated. Eventually will be removed.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GenericRepository<T> : IGenericRepository<T> where T : Model
{
    private readonly IConnectionFactory connectionFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public IMapper Mapper { get; set; }

    public IDatabaseSession DatabaseSession { get; set; }

    public IQueryBuilderFactory QueryBuilderFactory { get; set; }
    protected ISqlQueryBuilder SqlQueryBuilder => QueryBuilderFactory.SqlQueryBuilder();
    protected int HeaderBranchId { get; }
    protected string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();
    protected string TableName => SchemaName + "." + typeof(T).Name;

    protected QueryFactory QueryFactory { get; set; }
    protected IDbConnection Connection => connectionFactory.Connection;

    public GenericRepository(
        IHttpContextAccessor httpContextAccessor,
        IConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
        this.httpContextAccessor = httpContextAccessor;
        HeaderBranchId = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["BranchId"]);
        QueryFactory = new QueryFactory(Connection, new PostgresCompiler());
    }

    public virtual async Task<int> DeleteAsync(int id)
    {
        var affected = await QueryFactory
            .Query<T>()
            .Where("id", id)
            .UpdateAsync(new
            {
                isarchived = true
            });

        return affected;
    }

    public virtual async Task<int> DeletePermanentAsync(int id)
    {
        var sql = "DELETE FROM " + TableName + " WHERE id = @Id";
        return await Connection.ExecuteAsync(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        string sql = "SELECT * FROM " + TableName + " WHERE isarchived = false";
        if (typeof(TransactionModel).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
        {
            sql += " AND branchid = " + HeaderBranchId;
        }
        sql += " ORDER BY createddate desc ";
        return await Connection.QueryAsync<T>(sql);
    }

    public virtual async Task<T> GetAsync(int id)
    {
        var sql = "SELECT * FROM " + TableName + " WHERE  isarchived = false and id = @Id";
        return await Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public virtual async Task<int> InsertAsync(T obj)
    {
        InsertModelProperties(ref obj);
        var sql = new QueryBuilder<T>().GenerateInsertQuery();
        return await Connection.QueryFirstOrDefaultAsync<int>(sql, obj);
    }

    public virtual async Task<int> BulkInsertAsync(List<T> objs)
    {
        objs.ForEach(t => InsertModelProperties(ref t));
        var sql = new QueryBuilder<T>().GenerateInsertQuery();
        return await Connection.ExecuteAsync(sql, objs.AsEnumerable());
    }

    public virtual async Task<int> BulkUpdateAsync(List<T> objs)
    {
        objs.ForEach(t => InsertModelProperties(ref t));
        var sql = new QueryBuilder<T>().GenerateUpdateQuery();
        return await Connection.ExecuteAsync(sql, objs.AsEnumerable());
    }

    public void InsertModelProperties(ref T obj)
    {
        var accesor = HttpHelper.HttpContext.Request;
        obj.CreatedDate = DateTime.UtcNow;
        obj.IsArchived = false;
    }

    public virtual async Task<int> UpdateAsync(T obj)
    {
        UpdateModelProperties(ref obj);

        var sql = new QueryBuilder<T>().GenerateUpdateQuery();
        return await Connection.ExecuteAsync(sql, obj);
    }

    public void UpdateModelProperties(ref T obj)
    {
        obj.ModifiedDate = DateTime.UtcNow;
    }

    public async Task<int> InsertAuditAsync(object obj, int parentID, int auditId = 0)
    {
        var auditsql = new QueryBuilder<T>().GenerateInsertQueryForAudit("INSERT", parentID, auditId);

        return (int)await Connection.ExecuteScalarAsync(auditsql, obj);
    }
}

public abstract class GRepository<T> : IGenericRepository<T> where T : Model
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IConnectionFactory connectionFactory;

    protected QueryFactory QueryFactory { get; set; }
    protected IDbConnection Connection => connectionFactory.Connection;

    protected string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();
    protected string TableName => typeof(T).Name;

    //TODO: Depreciated. it will be removed eventually.
    public IDatabaseSession DatabaseSession { get; set; }
    public IMapper Mapper { get; set; }
    public IQueryBuilderFactory QueryBuilderFactory { get; set; }
    public ISqlQueryBuilder SqlQueryBuilder => QueryBuilderFactory.SqlQueryBuilder();
    //

    public GRepository(
        IHttpContextAccessor httpContextAccessor,
        IConnectionFactory connectionFactory)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.connectionFactory = connectionFactory;
        QueryFactory = new QueryFactory(Connection, new ChefPostgresCompiler());
    }

    public async Task<int> DeleteAsync(int id)
    {
        var affected = await QueryFactory
            .Query<T>()
            .Where("id", id)
            .UpdateDefaults()
            .UpdateAsync(new
            {
                isarchived = true
            });

        if (typeof(T) is IAuditable)
        {
           await InsertAuditLogAsync(new { isarchived = true }, AuditAction.Delete, id);
        }

        return affected;
    }

    public async Task<int> DeletePermanentAsync(int id)
    {
        return await QueryFactory
            .Query<T>()
            .Where("id", id)
            .DeleteAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await QueryFactory
            .Query<T>()
            .WhereNotArchived()
            .GetAsync<T>();
    }

    public async Task<T> GetAsync(int id)
    {
        return await QueryFactory
            .Query<T>()
            .Where("id", id)
            .WhereNotArchived()
            .FirstOrDefaultAsync<T>();
    }

    public async Task<int> InsertAsync(T obj)
    {
        var id = await QueryFactory
            .Query<T>()
            .InsertDefaults<T>(ref obj)
            .InsertGetIdAsync<int>(obj);

        if (obj is IAuditable)
        {
            await InsertAuditLogAsync(obj, AuditAction.Insert, id);
        }

        return id;
    }

    public async Task<int> BulkInsertAsync(List<T> objs)
    {
        //TODO: change this to sqlkata.
        objs.ForEach(t => InsertDefaults(ref t));
        var sql = new QueryBuilder<T>().GenerateInsertQuery();
        return await Connection.ExecuteAsync(sql, objs.AsEnumerable());
    }

    public async Task<int> UpdateAsync(T obj)
    {
        var affected = await QueryFactory
            .Query<T>()
            .UpdateDefaults<T>(ref obj)
            .Where("id", obj.Id)
            .UpdateAsync(obj);

        if (obj is IAuditable)
        {
          await  InsertAuditLogAsync(obj, AuditAction.Update, obj.Id);
        }

        return affected;
    }

    public async Task<int> BulkUpdateAsync(List<T> objs)
    {
        //TODO : change this to sql kata.
        objs.ForEach(t => UpdateDefaults(ref t));
        var sql = new QueryBuilder<T>().GenerateUpdateQuery();
        return await Connection.ExecuteAsync(sql, objs.AsEnumerable());
    }

    public Task<int> InsertAuditAsync(object obj, int parentID, int auditId = 0)
    {
        throw new NotImplementedException();
    }

    public void InsertDefaults(ref T obj)
    {
        obj.CreatedDate = DateTime.UtcNow;
        obj.IsArchived = false;
        obj.CreatedBy = "System";
    }

    public void UpdateDefaults(ref T obj)
    {
        obj.ModifiedDate = DateTime.UtcNow;
        obj.ModifiedBy = "System";
    }

    private IEnumerable<string> Columns
    {
        get
        {
            var columns = new List<string>();
            typeof(T).GetProperties().ToList().ForEach(p => columns.Add(p.Name));
            return columns.AsEnumerable();
        }
    }

    private async Task<int> InsertAuditLogAsync(Object obj, string action, int tablePK)
    {
       return await QueryFactory
            .Query<AuditLog>()
            .InsertAsync(new AuditLog()
            {
                Action = action,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = HttpHelper.Username,
                TablePK = tablePK,
                Values = JsonSerializer.Serialize(obj),
                SchemaName = SchemaName,
                TableName = TableName
            });
    }
}

public abstract class ConsoleRepository<T> : GRepository<T> where T : Model
{
    public ConsoleRepository(
        IHttpContextAccessor httpContextAccessor,
        IConsoleConnectionFactory consoleConnectionFactory)
        : base(httpContextAccessor, consoleConnectionFactory)
    {
    }
}

public abstract class TenantRepository<T> : GRepository<T> where T : Model
{
    public int headerBranchId = 0;

    public TenantRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory tenantConnectionFactory)
        : base(httpContextAccessor, tenantConnectionFactory)
    {
        this.headerBranchId = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["BranchId"]);
    }
}

public class ChefPostgresCompiler : PostgresCompiler
{
    protected override string OpeningIdentifier { get; set; } = "";
    protected override string ClosingIdentifier { get; set; } = "";
}