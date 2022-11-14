namespace Chef.Common.Data.Services;

public interface ICommonDataService : IBaseService
{
    Task<IEnumerable<Branch>> GetBranches();
}