namespace Chef.Common.Data.Repositories;

public class CommonDataRepository : TenantRepository<Model>, ICommonDataRepository
{
    public CommonDataRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory tenantConnectionFactory)
        : base(httpContextAccessor, tenantConnectionFactory)
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

