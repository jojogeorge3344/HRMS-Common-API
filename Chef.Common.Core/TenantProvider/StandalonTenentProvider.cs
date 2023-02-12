

using System.Linq;
using Chef.Common.Models;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Core
{
	public class StandalonTenentProvider :  ITenantProvider
	{
        private readonly IConfiguration configuration;

        public StandalonTenentProvider()
        {
            var builder = new ConfigurationBuilder()
                   .AddJsonFile("tenants.json", optional: true, reloadOnChange: true);

            this.configuration = builder.Build();
        }

        public Tenant Get(string host)
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            return tenants.FirstOrDefault(t => t.Host.Equals("default"));
        }

        public string GetConsoleConnectionString()
        {
            return configuration.GetSection("ConsoleConnection").Value;
        }

        public Tenant GetCurrent()
        {
            //var host = httpContext.Request.Host.Value.ToLower();
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            return tenants.FirstOrDefault(t => t.Host.Equals("default"));
        }

        public string GetModuleHost(string name)
        {
            var tenant = GetCurrent();
            return tenant.Modules.FirstOrDefault(m => m.Name == name).Host;
        }
    }
}

