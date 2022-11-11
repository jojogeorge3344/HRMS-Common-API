using Chef.Common.Models;
using Chef.Common.Services;

namespace Chef.Common.Data.Services;

public interface ICommonDataService : IBaseService
{
    Task<IEnumerable<Branch>> GetBranches();
}