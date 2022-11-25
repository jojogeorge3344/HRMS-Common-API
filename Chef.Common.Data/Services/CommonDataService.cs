using Chef.Common.Authentication.Models;
using Chef.Common.Data.Repositories;
using Chef.Common.Models;

namespace Chef.Common.Data.Services;

public class CommonDataService : ICommonDataService
{
    private readonly ICommonDataRepository commonDataRepository;
 
    public CommonDataService(ICommonDataRepository commonDataRepository)
    {
        this.commonDataRepository = commonDataRepository; 
    }

    public Task<IEnumerable<BranchViewModel>> GetBranches()
    {
        return commonDataRepository.GetBranches();
    }

    public async Task<IEnumerable<UserBranchDto>> GetMyBranches()
    {
        return await commonDataRepository.GetBranches(HttpHelper.Username);
    }
    public async Task<CompanyDetails> GetMyCompany()
    {
		return await commonDataRepository.GetMyCompany();
	}
}

