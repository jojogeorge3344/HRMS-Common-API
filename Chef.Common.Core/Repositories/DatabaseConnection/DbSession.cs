using System;
using System.Data;
using Chef.Common.Core;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Chef.Common.Repositories
{
    public sealed class DbSession : IDisposable
    {
        private readonly Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        private readonly IHttpContextAccessor context;
        private readonly IAppConfiguration appConfiguration;

        public DbSession(IAppConfiguration appConfiguration, IHttpContextAccessor context)
        {
            this.context = context;
            this.appConfiguration = appConfiguration;

            _id = Guid.NewGuid();
            Connection = DBConnection;
            Connection.Open();
        }

        public IDbConnection DBConnection => new NpgsqlConnection(GetConnectionString());

        public string HostName => context.HttpContext?.Request.Host.Value.ToLower();

        private string GetConnectionString()
        {
            return appConfiguration.GetTenant(HostName).ConnectionString;
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}