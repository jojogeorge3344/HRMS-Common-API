using System.Collections.Generic;
using System.Linq;
using Chef.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Core
{
    public class TenantProvider: ITenantProvider
    {
        private readonly IConfiguration configuration;
        private readonly HttpContext httpContext;

        public TenantProvider(
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor?.HttpContext;

            //load the tenants
            var builder = new ConfigurationBuilder()
                   .AddJsonFile($"tenants.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddJsonFile("tenants.json", optional: true, reloadOnChange: true);

            this.configuration = builder.Build();
        }

        public string GetConsoleConnectionString()
        {
            return configuration.GetSection("ConsoleConnection").Value;
        }

        public Tenant Get(string host)
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            return tenants.FirstOrDefault(t => host.Contains(t.Host));
        }

        public Tenant GetCurrent()
        {
            var host = httpContext.Request.Host.Value.ToLower();
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            return tenants.FirstOrDefault(t => host.Contains(t.Host));
        }

        public string GetModuleHost(string name)
        {
            var tenant = GetCurrent();
            return tenant.Modules.FirstOrDefault(m => m.Name == name).Host;
        }
    }
}
