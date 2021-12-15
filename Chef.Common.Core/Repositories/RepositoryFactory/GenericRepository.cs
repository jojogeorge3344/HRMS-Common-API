using Chef.Common.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace Chef.Common.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Model
    { 

        private DbSession _session;
        private IHttpContextAccessor httpContextAccessor;
        public int headerBranchId = 0;
        public GenericRepository(IHttpContextAccessor httpContextAccessor, DbSession session)
        {
            _session = session;
            this.httpContextAccessor = httpContextAccessor;
            this.headerBranchId = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["BranchId"]);
        }

        public IDbConnection Connection
        {
            get
            {
                return _session.Connection;
            }
        }

        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();

        public string TableName => SchemaName + "." + typeof(T).Name;

        public async virtual Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM " + TableName + " WHERE id = @Id";
            return await Connection.ExecuteAsync(sql, new { Id = id });
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = "SELECT * FROM " + TableName + " WHERE isarchived=false ";
            if (typeof(TransactionModel).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                sql += " AND branchid = " + this.headerBranchId;
            }
            sql += " ORDER BY createddate desc ";
            return await Connection.QueryAsync<T>(sql);
        }

       
        public async virtual Task<T> GetAsync(int id)
        {
            var sql = "SELECT * FROM " + TableName + " WHERE  isarchived=false and id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async virtual Task<T> InsertAsync(T obj)
        {
            InsertModelProperties(ref obj);
            try
            {
                var sql = new QueryBuilder<T>().GenerateInsertQuery();
                var result = await Connection.QueryFirstOrDefaultAsync<T>(sql, obj);
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

        public async virtual Task<List<T>> BulkInsertAsync(List<T> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                var tmp = objs[i];
                InsertModelProperties(ref tmp);
                objs[i] = tmp;
            }
            try
            {
                var sql = new QueryBuilder<T>().GenerateInsertQuery();
                var result = await Connection.ExecuteAsync(sql, objs.AsEnumerable());
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
            obj.CreatedBy = "system";
        }

        public async virtual Task<int> UpdateAsync(T obj)
        {
            UpdateModelProperties(ref obj);
            try
            {
                var sql = new QueryBuilder<T>().GenerateUpdateQuery();
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
            obj.ModifiedBy = "system";
            obj.ModifiedDate = DateTime.UtcNow;
        }
    }
}