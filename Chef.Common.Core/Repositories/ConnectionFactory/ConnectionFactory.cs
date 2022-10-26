using System.Data;
using Chef.Common.Core;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Chef.Common.Repositories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IHttpContextAccessor context;
        private readonly IAppConfiguration appConfiguration;

        public ConnectionFactory(
            IAppConfiguration appConfiguration,
            IHttpContextAccessor context)
        {
            this.context = context;
            this.appConfiguration = appConfiguration;
        }

        public IDbConnection Connection
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

        private string GetConnectionString()
        {
            return appConfiguration.GetTenant(HostName).ConnectionString;
        }
    }
}