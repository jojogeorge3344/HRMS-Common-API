using Chef.Common.Authentication.Models;

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

    public async Task<IEnumerable<UserBranchDto>> GetBranches(string userName)
    {
        return await QueryFactory
            .Query<UserBranch>()
            .Join<Branch, UserBranch>()
            .Select(
                "userbranch.username",
                "userbranch.branchid",
                "branch.name",
                "branch.code")
            .Where(new {
                isactive = true,
                username = userName
            })
            .WhereNotArchived()
            .GetAsync<UserBranchDto>();
    }
}

