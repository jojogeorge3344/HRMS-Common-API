using Chef.Common.Core;
using Chef.Common.Models;
using Chef.Common.Repositories;
using SqlKata.Execution;

namespace Chef.Common.Data.Repositories;

public class CommonDataRepository : TenantGenericRepository, ICommonDataRepository
{
    public CommonDataRepository(ITenantConnectionFactory tenantConnectionFactory)
        : base(tenantConnectionFactory)
    {
    }

    public async Task<IEnumerable<Branch>> GetBranches()
    {
        return await QueryFactory
            .Query<Branch>()
            .Select("id", "name", "code")
            .Where("isactive", true)
            .WhereNotArchived()
            .GetAsync<Branch>();
    }
}

