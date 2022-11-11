using System;
using System.Data;
using Chef.Common.Core;
using Npgsql;

namespace Chef.Common.Repositories
{
    public class TenantConnectionFactory : ITenantConnectionFactory, IDisposable
    {
        private readonly Guid _id;
        private readonly ITenantProvider tenantProvider;

        public TenantConnectionFactory(ITenantProvider tenantProvider)
        {
            this.tenantProvider = tenantProvider;

            _id = Guid.NewGuid();
            Connection.Open();
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(GetConnectionString());
            }
        }

        public IDbTransaction Transaction { get; set; }

        private string GetConnectionString()
        {
            return tenantProvider.GetCurrent().ConnectionString;
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}