using System.Collections.Generic;
using System.Linq;
using Chef.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Core
{
    public class TenantProvider: ITenantProvider
    {
        private readonly IConfiguration configuration;

        public TenantProvider(IWebHostEnvironment environment)
        {
            var path = System.IO.Path.Combine(
                environment.ContentRootPath,
                "..",
                "..",
                "chef.common",
                "Chef.Common.Core");

            //load the tenants
            var builder = new ConfigurationBuilder()
                   .SetBasePath(environment.ContentRootPath)
                   .AddJsonFile($"{path}/tenants.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddJsonFile("tenants.json", optional: true, reloadOnChange: true);

            this.configuration = builder.Build();
        }

        public Tenant Get(string host)
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();
            return tenants.FirstOrDefault(t => host.Contains(t.Host));
        }
    }
}
