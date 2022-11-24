using Chef.Common.Authentication.Models;

namespace Chef.Common.Data.Repositories;

public interface ICommonDataRepository : IRepository
{
    Task<IEnumerable<BranchViewModel>> GetBranches();
    Task<IEnumerable<UserBranchDto>> GetBranches(string userName);
   
    
}

