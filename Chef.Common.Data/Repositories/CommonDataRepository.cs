namespace Chef.Common.Data.Repositories;

public class CommonDataRepository : TenantGenericRepository, ICommonDataRepository
{
    public CommonDataRepository(ITenantConnectionFactory tenantConnectionFactory)
        : base(tenantConnectionFactory)
    {
    }
}

