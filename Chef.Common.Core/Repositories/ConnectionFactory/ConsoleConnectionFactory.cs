using System;
using System.Data;
using Chef.Common.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Chef.Common.Core.Repositories;

public class ConsoleConnectionFactory : IConsoleConnectionFactory, IDisposable
{
    private readonly Guid _id;
    private readonly ITenantProvider tenantProvider;
    private readonly IDbConnection dbConnection;

    public ConsoleConnectionFactory(ITenantProvider tenantProvider)
    {
        this.tenantProvider = tenantProvider;

        _id = Guid.NewGuid();
        dbConnection = new NpgsqlConnection(tenantProvider.GetConsoleConnectionString());
        dbConnection.Open();
    }

    public IDbConnection Connection
    {
        get
        {
            return dbConnection;
        }
    }

    public IDbTransaction Transaction { get; set; }

    public void Dispose()
    {
        dbConnection?.Close();
        dbConnection?.Dispose();
    }
}