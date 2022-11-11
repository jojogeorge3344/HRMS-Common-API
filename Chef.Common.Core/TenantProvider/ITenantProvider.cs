using Chef.Common.Models;

namespace Chef.Common.Core;

public interface ITenantProvider
{
    string GetConsoleConnectionString();
    Tenant Get(string host);
    Tenant GetCurrent();
    string GetModuleHost(string name);
}
