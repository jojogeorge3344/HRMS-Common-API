
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;

public class IntegrationDetailsRepository : TenantRepository<IntegrationDetails>, IIntegrationDetailsRepository
{
    public IntegrationDetailsRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session) : base(httpContextAccessor, session)
    {
    }
}
