using Chef.Common.Models;
using Chef.Common.Repositories;

namespace Chef.Common.Data.Repositories;

public interface ICommonDataRepository : IRepository
{
    Task<IEnumerable<Branch>> GetBranches();
}

