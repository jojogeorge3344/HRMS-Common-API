using Chef.Common.Models;

namespace Chef.Common.Core;

public interface ITenantProvider
{
    Tenant Get(string host);
    Tenant GetCurrent();
}
