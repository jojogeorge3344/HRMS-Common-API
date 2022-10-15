using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api
{
    public interface IAdminApi
    {
        [Get("/Role/GetAllRoles")]
        Task<IEnumerable<Role>> GetAllRoles();

        [Get("/Role/GetAllNodeBasedRoles/{nodeId}")]
        Task<IEnumerable<NodeBasedRoleView>> GetAllNodeBasedRoles(int nodeId);

        [Get("/User/GetUserRoles/{id}")]
        Task<IEnumerable<UserRoleViewModel>> GetUserRoles(int id);

        [Get("/User/GetAllUsers")]
        Task<IEnumerable<User>> GetAllUsers();

        [Get("/Node/GetAllSubModules/{moduleId}")]
        Task<IEnumerable<Node>> GetAllSubModules(int moduleId);

        [Get("/Node/GetNodeID/{nodeName}")]
        Task<int> GetNodeId(string nodeName);

        [Get("/NodeField/GetAllNodeControlFields/{nodeId}")]
        Task<IEnumerable<NodeControlField>> GetAllNodeControlFields(int nodeId);

        [Get("/User/GetUserNodes/{id}")]
        Task<IEnumerable<UserPermission>> GetUserNodes(int userId);

        //TODO - check the multiple parameters
        [Get("/User/GetUserRolesByUserIdAndNodename/{userId}/{nodeName}")]
        Task<IEnumerable<UserRoleDto>> GetUserRolesByUserIdAndNodename(int userId, string nodeName);

        [Get("User/GetUserRolesByUserId/{userId}")]
        Task<IEnumerable<UserMetaData>> GetUserRolesByUserId(int userId);
    }
}
