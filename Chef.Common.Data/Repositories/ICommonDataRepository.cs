namespace Chef.Common.Data.Repositories;

public interface ICommonDataRepository : IRepository
{
    Task<IEnumerable<Branch>> GetBranches();
}

