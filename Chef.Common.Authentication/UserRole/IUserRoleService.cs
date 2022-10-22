using Chef.Common.Models;

namespace Chef.Common.Authentication
{
    public interface IUserRoleService
	{
        Task<IEnumerable<UserPermission>> GetUserNodesAsync(int id);
        Task<IEnumerable<UserRoleDto>> GetUserRolesByUserIdAndNodeNameAsync(int userId, string nodeName);
        Task<IEnumerable<UserMetaData>> GetUserRolesByUserIdAsync(int id);
    }
}
