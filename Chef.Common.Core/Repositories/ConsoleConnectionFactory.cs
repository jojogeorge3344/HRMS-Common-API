using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chef.Common.Exceptions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Repositories
{
    public class ConsoleConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;
        private readonly IHttpContextAccessor context;
        private readonly IConfiguration configuration;

        public ConsoleConnectionFactory(IConfiguration configuration, IHttpContextAccessor context)
        {
            this.context = context;
            this.configuration = configuration;
            this.connectionString = GetConnectionString();
        }

        public string HostName
        {
            get
            {
                return context.HttpContext.Request.Host.Value.ToLower();
            }
        }

        IDbConnection IConnectionFactory.Connection => throw new NotImplementedException();

        public string GetConnectionString()
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            var currentTenant = tenants.FirstOrDefault(t => t.Host.ToLower().Equals(HostName));

            if (currentTenant != null)
            {
                return currentTenant.ConnectionString;
            }
            else
            {
                throw new TenantNotFoundException(HostName + " not configured properly.");
            }
        }

        //    public IDbConnection Connection
        //    {
        //        get
        //        {
        //            return new NpgsqlConnection(connectionString);
        //        }
        //    }
        //}
    }
}