using Chef.Common.Models;

namespace Chef.Common.Authentication
{
    public class UserRoleService : IUserRoleService
	{
        private readonly IUserRoleRepository userRoleRepository;

		public UserRoleService(IUserRoleRepository userRoleRepository)
		{
            this.userRoleRepository = userRoleRepository;
		}

        public Task<IEnumerable<UserPermission>> GetUserNodesAsync(int id)
        {
            return this.userRoleRepository.GetUserNodesAsync(id);
        }

        public Task<IEnumerable<UserRoleDto>> GetUserRolesByUserIdAndNodeNameAsync(int userId, string nodeName)
        {
            return this.userRoleRepository.GetUserRolesByUserIdAndNodenameAsync(userId, nodeName);
        }

        public Task<IEnumerable<UserMetaData>> GetUserRolesByUserIdAsync(int id)
        {
            return this.userRoleRepository.GetUserRolesByUserIdAsync(id);
        }
    }
}

