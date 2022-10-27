using System;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using Chef.Common.Core;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Chef.Common.Repositories
{
    public class TenantConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly Guid _id;
        private readonly IHttpContextAccessor context;
        private readonly IAppConfiguration appConfiguration;

        public TenantConnectionFactory(
            IAppConfiguration appConfiguration,
            IHttpContextAccessor context)
        {
            this.context = context;
            this.appConfiguration = appConfiguration;

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
            var hostName = context.HttpContext?.Request.Host.Value.ToLower(); 
            return appConfiguration.GetTenant(hostName).ConnectionString;
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}