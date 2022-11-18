using Chef.Common.Authentication.Models;

namespace Chef.Common.Data.Services;

public interface ICommonDataService : IBaseService
{
    Task<IEnumerable<Branch>> GetBranches();
    Task<IEnumerable<UserBranchDto>> GetMyBranches();
}