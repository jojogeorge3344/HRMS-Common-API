using System;
using System.Data;
using Npgsql;

namespace Chef.Common.Core.Repositories;

public class TenantConnectionFactory : ITenantConnectionFactory, IDisposable
{
    private readonly Guid _id;
    private readonly ITenantProvider tenantProvider;
    private readonly IDbConnection dbConnection;
    private bool disposed = false;

    public TenantConnectionFactory(ITenantProvider tenantProvider)
    {
        this.tenantProvider = tenantProvider;

        _id = Guid.NewGuid();
        dbConnection = new NpgsqlConnection(tenantProvider.GetCurrent().ConnectionString);
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
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                dbConnection?.Close();
                dbConnection?.Dispose();
            }

            disposed = true;
        }
    }
}