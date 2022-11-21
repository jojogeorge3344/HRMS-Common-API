namespace Chef.Common.DMS.Repositories;

public class ZooKeeperRepository : TenantRepository<Model>, IZooKeeperRepository
{
    public ZooKeeperRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connection)
        : base(httpContextAccessor, connection)
    {
    }

    public void CreateSchema()
    {
        StringBuilder fullQuery = new StringBuilder();

        string query = "CREATE SCHEMA IF NOT EXISTS dms;";
        Connection.Execute(query);
        fullQuery.Append(query);

        query = new QueryBuilder<FileVersion>().GenerateCreateTableQuery();
        Connection.Execute(query);
        fullQuery.Append(query);

        query = new QueryBuilder<FileDetail>().GenerateCreateTableQuery();
        Connection.Execute(query);
        fullQuery.Append(query);

        query = new QueryBuilder<FileMetaData>().GenerateCreateTableQuery();
        Connection.Execute(query);
        fullQuery.Append(query);

        System.IO.File.WriteAllText(@"DMSTableQuery.sql", fullQuery.ToString());
    }
}
