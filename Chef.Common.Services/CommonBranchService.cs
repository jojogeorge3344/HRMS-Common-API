using System;
using Chef.Common.Model.Dtos;
using Chef.Common.Repositories;

namespace Chef.Common.Services
{
    public class CommonBranchService : ICommonBranchService
    {
        private readonly ICommonBranchRepository branchRepository;

        public CommonBranchService(ICommonBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public Task<BranchDto> Get(int id)
        {
            return this.branchRepository.Get(id);
        }

        public Task<BranchDto> Get(string code)
        {
            return this.Get(code);
        }

        public Task<IEnumerable<BranchDto>> GetAll()
        {
            return this.branchRepository.GetAll();
        }

        public Task<IEnumerable<UserBranchDto>> GetAllUserBranches(int userId)
        {
            return this.branchRepository.GetAllUserBranches(userId);
        }
    }
}

