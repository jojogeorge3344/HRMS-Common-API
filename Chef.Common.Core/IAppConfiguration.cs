using System.Collections.Generic;
using Chef.Common.Models;

namespace Chef.Common.Core
{
    public interface IAppConfiguration
    {
        TenantDto GetTenant(string host);
        string GetTenantModuleHost(string host, string moduleName);
    }
}
