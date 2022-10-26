using System;
using System.Collections.Generic;
using System.Linq;
using Chef.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Core
{
    public class AppConfiguration: IAppConfiguration
    {
        private readonly IConfiguration configuration;

        public AppConfiguration(IWebHostEnvironment environment)
        {
            var path = System.IO.Path.Combine(
                environment.ContentRootPath,
                "..",
                "..",
                "chef.common",
                "Chef.Common.Core");

            //load the tenant 
            var builder = new ConfigurationBuilder()
                   .SetBasePath(environment.ContentRootPath)
                   .AddJsonFile($"{path}/tenants.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddJsonFile("tenants.json", optional: true, reloadOnChange: true);

            this.configuration = builder.Build();
        }

        public TenantDto GetTenant(string host)
        {
            var tenants = configuration.GetSection("Tenants").Get<List<TenantDto>>();
            return tenants.FirstOrDefault(t => host.Contains(t.Host));
        }

        public string GetTenantModuleHost(string host, string moduleName)
        {
            var tenant = GetTenant(host);
            return tenant.Modules.FirstOrDefault(m => m.Name.Equals(moduleName)).Host;
        }
    }
}
