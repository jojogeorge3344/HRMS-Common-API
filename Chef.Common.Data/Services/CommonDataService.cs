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

    public Task<IEnumerable<Branch>> GetBranches()
    {
        return commonDataRepository.GetBranches();
    }
}

