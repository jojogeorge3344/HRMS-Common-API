using Chef.Common.Authentication.Models;
using Chef.Common.Core.Extensions;
using Chef.Common.Models;
using Chef.Common.Repositories;
using SqlKata;

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
              .Query<UserBranch>().Join("common.branch", "branch.id", "userbranch.branchid")
              .Select(
                  "userbranch.username",
                  "userbranch.branchid",
                  "userbranch.IsDefault",
                  "branch.name as BranchName",
                  "branch.code as BranchCode")
              .Where(new
              {
                  username = userName,

              })
              .WhereFalse("branch.isArchived")
             .WhereFalse("userbranch.isArchived")
             .GetAsync<UserBranchDto>();
    }


    public async Task<CompanyDetails> GetMyCompany()
    {
        return await QueryFactory
                 .Query<CompanyDetails>()
                 .WhereNotArchived()
                .FirstOrDefaultAsync<CompanyDetails>();
    }


    public async Task<IEnumerable<ReasonCodeMaster>> GetAllReasonCode()
    {
        return await QueryFactory
            .Query<ReasonCodeMaster>()
            .Select("reasoncodemaster.reasoncode", "reasoncodemaster.remarks")
            .WhereNotArchived()
            .GetAsync<ReasonCodeMaster>();
    }
}