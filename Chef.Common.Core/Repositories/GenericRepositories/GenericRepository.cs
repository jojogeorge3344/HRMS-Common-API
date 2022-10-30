using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Chef.Common.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Chef.Common.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Model
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IMapper Mapper { get; set; }

        public IDatabaseSession DatabaseSession { get; set; }
        public IQueryBuilderFactory QueryBuilderFactory { get; set; }
        public ISqlQueryBuilder SqlQueryBuilder => QueryBuilderFactory.SqlQueryBuilder();
        protected int HeaderBranchId { get; }
        protected IDbConnection Connection => connectionFactory.Connection;
        protected string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();
        protected string TableName => SchemaName + "." + typeof(T).Name;

        protected QueryFactory QueryFactory { get; set; }

        public GenericRepository(
            IHttpContextAccessor httpContextAccessor
            , IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            this.httpContextAccessor = httpContextAccessor;
            HeaderBranchId = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["BranchId"]);
            QueryFactory = new QueryFactory(Connection, new PostgresCompiler());
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            string sql = "DELETE FROM " + TableName + " WHERE id = @Id";
            return await Connection.ExecuteAsync(sql, new { Id = id });
        }

        public virtual async Task<int> ArchiveAsync(int id)
        {
            string sql = "UPDATE " + TableName + " SET isarchived = true WHERE id = @Id";
            return await Connection.ExecuteAsync(sql, new { Id = id });
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            string sql = "SELECT * FROM " + TableName + " WHERE isarchived = false ";
            if (typeof(TransactionModel).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                sql += " AND branchid = " + HeaderBranchId;
            }
            sql += " ORDER BY createddate desc ";
            return await Connection.QueryAsync<T>(sql);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE  isarchived = false and id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public virtual async Task<int> InsertAsync(T obj)
        {
            InsertModelProperties(ref obj);
            string sql = new QueryBuilder<T>().GenerateInsertQuery();
            return await Connection.QueryFirstOrDefaultAsync<int>(sql, obj);
        }

        public virtual async Task<int> BulkInsertAsync(List<T> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                T tmp = objs[i];
                InsertModelProperties(ref tmp);
                objs[i] = tmp;
            }

            try
            {
                string sql = new QueryBuilder<T>().GenerateInsertQuery();
                return await Connection.ExecuteAsync(sql, objs.AsEnumerable());
            }
            catch
            {
                throw;
            }
        }

        public void InsertModelProperties(ref T obj)
        {
            obj.CreatedDate = DateTime.UtcNow;
            obj.IsArchived = false;
        }

        public virtual async Task<int> UpdateAsync(T obj)
        {
            UpdateModelProperties(ref obj);

            string sql = new QueryBuilder<T>().GenerateUpdateQuery();
            return await Connection.ExecuteAsync(sql, obj);
        }

        public void UpdateModelProperties(ref T obj)
        {
            obj.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<int> InsertAuditAsync(object obj, int parentID, int auditId = 0)
        {
            string auditsql = new QueryBuilder<T>().GenerateInsertQueryForAudit("INSERT", parentID, auditId);

            return (int)await Connection.ExecuteScalarAsync(auditsql, obj);
        }
    }
}