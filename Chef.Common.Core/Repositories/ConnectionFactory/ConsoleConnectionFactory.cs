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

    public ConsoleConnectionFactory(ITenantProvider tenantProvider)
    {
        this.tenantProvider = tenantProvider;

        _id = Guid.NewGuid();
        Connection.Open();
    }

    public IDbConnection Connection
    {
        get
        {
            return new NpgsqlConnection(tenantProvider.GetConsoleConnectionString());
        }
    }

    public IDbTransaction Transaction { get; set; }

    public void Dispose()
    {
        Connection?.Close();
        Connection?.Dispose();
    }
}