using Chef.Common.Exceptions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Chef.Common.Repositories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;
        private readonly IHttpContextAccessor context;
        private readonly IConfiguration configuration;

        public ConnectionFactory(IConfiguration configuration, IHttpContextAccessor context)
        {
            this.context = context;
            this.configuration = configuration;
            this.connectionString = GetConnectionString();
        }
        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        public string HostName
        {
            get
            {
                return context.HttpContext?.Request.Host.Value.ToLower();
            }
        }

        public string GetConnectionString()
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            Tenant currentTenant = tenants.FirstOrDefault(t => t.Host.ToLower().Equals(HostName));
            if (currentTenant != null)
                return currentTenant.ConnectionString;
            else
                throw new TenantNotFoundException(HostName + " not configured properly.");
        }
    }
}