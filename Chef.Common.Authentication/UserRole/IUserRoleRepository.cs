using Chef.Common.Models;
using Chef.Common.Repositories;

namespace Chef.Common.Authentication
{
    public interface IUserRoleRepository : IGenericRepository<User>
    {
        Task<IEnumerable<UserPermission>> GetUserNodesAsync(int id);
        Task<IEnumerable<UserRoleDto>> GetUserRolesByUserIdAndNodenameAsync(int userId, string nodeName);
        Task<IEnumerable<UserMetaData>> GetUserRolesByUserIdAsync(int id);
    }
}

