using Chef.Common.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Model
    {
        private readonly IConnectionFactory connectionFactory;

        public GenericRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IDbConnection Connection
        {
            get
            {
                return connectionFactory.Connection;
            }
        }   
    
        public string SchemaName => typeof(T).Namespace.Split('.')[1].ToLower();

        public string TableName => SchemaName + "." + typeof(T).Name;

        public async Task<int> DeleteAsync(int id)
        {
            using (Connection)
            {
                var sql = "DELETE FROM " + TableName + " WHERE id = @Id";
                return await Connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (Connection)
            {
                var sql = "SELECT * FROM " + TableName + " ORDER BY id";
                return await Connection.QueryAsync<T>(sql);
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (Connection)
            {
                var sql = "SELECT * FROM " + TableName + " WHERE id = @Id";
                return await Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
            }
        }

        public async Task<T> InsertAsync(T obj)
        {
            InsertModelProperties(ref obj);

            using (Connection)
            {
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

                    throw ex;
                }
            }
        }

        public void InsertModelProperties(ref T obj)
        {
            obj.CreatedDate = DateTime.UtcNow;
            obj.IsArchived = false;
            obj.CreatedBy = "system";
        }

        public async Task<int> UpdateAsync(T obj)
        {
            UpdateModelProperties(ref obj);

            using (Connection)
            {
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

                    throw ex;
                }
            }
        }

        public void UpdateModelProperties(ref T obj)
        {
            obj.ModifiedBy = "system";
            obj.ModifiedDate = DateTime.UtcNow;
        }
    }
}