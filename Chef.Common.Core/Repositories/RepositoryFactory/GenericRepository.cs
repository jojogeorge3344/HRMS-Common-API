using AutoMapper;
using Chef.Common.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Model
    {
        public IMapper Mapper { get; set; }
        private readonly DbSession _session;
        private readonly IHttpContextAccessor httpContextAccessor;
        public int headerBranchId = 0;

        public IQueryBuilderFactory QueryBuilderFactory { get; set; }

        public ISqlQueryBuilder SqlQueryBuilder => QueryBuilderFactory.SqlQueryBuilder();

        public IDatabaseSession DatabaseSession { get; set; }

        public GenericRepository(IHttpContextAccessor httpContextAccessor, DbSession session)
        {
            _session = session;
            this.httpContextAccessor = httpContextAccessor;
            headerBranchId = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["BranchId"]);
        }

        public IDbConnection Connection => _session.Connection;

        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();

        public string TableName => SchemaName + "." + typeof(T).Name;

        public virtual async Task<int> DeleteAsync(int id)
        {
            string sql = "DELETE FROM " + TableName + " WHERE id = @Id";
            return await Connection.ExecuteAsync(sql, new { Id = id });
        }

        public virtual async Task<int> ArchiveAsync(int id)
        {
            string sql = "UPDATE " + TableName + " SET isarchived=true WHERE id = @Id";
            return await Connection.ExecuteAsync(sql, new { Id = id });
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            string sql = "SELECT * FROM " + TableName + " WHERE isarchived=false ";
            if (typeof(TransactionModel).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                sql += " AND branchid = " + this.headerBranchId;
            }
            sql += " ORDER BY createddate desc ";
            return await Connection.QueryAsync<T>(sql);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE  isarchived=false and id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public virtual async Task<T> InsertAsync(T obj)
        {
            InsertModelProperties(ref obj);
            try
            {
                string sql = new QueryBuilder<T>().GenerateInsertQuery();
                T result = await Connection.QueryFirstOrDefaultAsync<T>(sql, obj);
                obj.Id = Convert.ToInt32(result.Id);
                return obj;
            }
            catch (Exception ex)
            {
                bool value = ex.Message.Contains("duplicate key value violates unique constraint");

                if (value)
                {
                    obj.Id = -1;
                    return obj;
                }

                throw;
            }
        }

        public virtual async Task<List<T>> BulkInsertAsync(List<T> objs)
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
                int result = await Connection.ExecuteAsync(sql, objs.AsEnumerable());
                return objs;
            }
            catch (Exception ex)
            {
                bool value = ex.Message.Contains("duplicate key value violates unique constraint");

                if (value)
                {
                    return objs;
                }

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

            try
            {
                string sql = new QueryBuilder<T>().GenerateUpdateQuery();
                return await Connection.ExecuteAsync(sql, obj);
            }
            catch (Exception ex)
            {
                bool value = ex.Message.Contains("duplicate key value violates unique constraint");

                if (value)
                {
                    obj.Id = -1;
                    return obj.Id;
                }

                throw;
            }
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