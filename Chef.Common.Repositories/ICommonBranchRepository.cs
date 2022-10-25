using System;
using Chef.Common.Model.Dtos;

namespace Chef.Common.Repositories
{
	public interface ICommonBranchRepository
    {
        Task<BranchDto> Get(int id);
        Task<BranchDto> Get(string code);
        Task<IEnumerable<BranchDto>> GetAll();
        Task<IEnumerable<UserBranchDto>> GetAllUserBranches(int userId);
    }
}

