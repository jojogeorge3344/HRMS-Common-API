using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core
{
    public class AppConfiguration: IAppConfiguration
    {
        private readonly IConfiguration configuration;

        public AppConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetHostString(string Host, string Name)
        {
            var tenants = configuration.GetSection("Tenants").Get<List<Tenant>>();

            Tenant currentTenant = tenants.FirstOrDefault(t => t.Host.ToLower().Equals(Host));

            string Baseaddress = currentTenant.ApiClients.Where(x => x.Name.Equals(Name)).Select(x => x.BaseAddress).FirstOrDefault();
            Baseaddress = Baseaddress + "/";
            return Baseaddress;
        }
    }
}

