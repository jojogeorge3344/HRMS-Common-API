using Chef.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Chef.Common.Test
{
    public class TestConfiguration : ITestConfiguration
    {
        readonly IConfiguration configuration;

        public TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string HostName
        {
            get
            {
                var unittesttenantname = configuration.GetValue<string>("UnitTestTenantName");
                var tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()
                    .Where(t => t.Name.ToLower().Equals(unittesttenantname.ToLower())).FirstOrDefault();
                return tenant.Host;
            }
        }



    }
}
