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

    public async Task<IEnumerable<BranchViewModel>> GetBranches()
    {
        return await QueryFactory
            .Query<Branch>()
            .Select("id as BranchId", "name as BranchName", "code as BranchCode")
            .Where("isactive", true)
            .WhereNotArchived()
            .GetAsync<BranchViewModel>();
    }

    public async Task<IEnumerable<UserBranchDto>> GetBranches(string userName)
    {
        return await QueryFactory
            .Query<UserBranch>()
            .Join<Branch, UserBranch>()
            .Select(
                "userbranch.UserName",
                "userbranch.BranchId",
                "branch.name as BranchName",
                "branch.code as BranchCode")
            .Where(new
            {
                isactive = true,
                username = userName
            })
            .WhereNotArchived()
            .GetAsync<UserBranchDto>();
    }
}