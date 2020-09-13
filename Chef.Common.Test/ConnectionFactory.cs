using Chef.Common.Repositories;
using Chef.Common.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Chef.Common.Test
{

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
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

        public string GetConnectionString()
        {
            var unittesttenantname = configuration.GetValue<string>("UnitTestTenantName");
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            Tenant currentTenant = tenants.FirstOrDefault(t => t.Name.ToLower().Equals(unittesttenantname.ToLower()));
            if (currentTenant != null)
                return currentTenant.ConnectionString;
            else
                throw new Exception("Connection string not configured properly. Set HostName to *");
        }
    }

}
