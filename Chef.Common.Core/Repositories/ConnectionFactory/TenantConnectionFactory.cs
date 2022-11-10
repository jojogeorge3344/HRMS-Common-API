using System;
using System.Data;
using Chef.Common.Core;
using Npgsql;

namespace Chef.Common.Repositories
{
    public class TenantConnectionFactory : ITenantConnectionFactory, IDisposable
    {
        private readonly Guid _id;
        private readonly ITenantProvider tenandProvider;

        public TenantConnectionFactory(ITenantProvider tenantProvider)
        {
            this.tenandProvider = tenantProvider;

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
            return tenandProvider.GetCurrent().ConnectionString;
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}