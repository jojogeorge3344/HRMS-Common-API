using Chef.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Chef.Common.Test
{
    public class TestConfiguration : ITestConfiguration
    {
        private readonly IConfiguration configuration;

        public TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string HostName
        {
            get
            {
                string unittesttenantname = configuration.GetValue<string>("UnitTestTenantName");
                Tenant tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()
                    .Where(t => t.Name.ToLower().Equals(unittesttenantname.ToLower())).FirstOrDefault();
                return tenant.Host;
            }
        }



    }
}
