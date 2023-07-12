using System;
using SqlKata;
using System.Threading;
using System.Threading.Tasks;
using Chef.Common.Repositories;
using System.Collections.Generic;

namespace Chef.Common.Repositories;

public partial class DatabaseSession
{
    public Task<TOutput> InsertWithAuditLogAsync<TOutput>(
        Query query,
        CancellationToken cancellationToken = default)
    {
        SqlResult sqlResult = query.Compile();
        var id = ExecuteScalarAsync<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);

        var sql = sqlResult.Sql;


        return id;
    }

    private string GenerateAuditSql(string sql)
    {
        var table = sql.Split(" ")[2].Split(".");
        var auditTableName = "audit." + table[1] + "_audit";

        return "";
    }
}

