using System;
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
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        private readonly IHttpContextAccessor context;
        private readonly IConfiguration configuration;

        public DbSession(IConfiguration configuration, IHttpContextAccessor context)
        {
            this.context = context;
            this.configuration = configuration;
            
            _id = Guid.NewGuid();
            Connection = DBConnection;
            Connection.Open();
        }

        public IDbConnection DBConnection
        {
            get
            {
                return new NpgsqlConnection(GetConnectionString());
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

        public void Dispose() => Connection?.Dispose();
    }
}
