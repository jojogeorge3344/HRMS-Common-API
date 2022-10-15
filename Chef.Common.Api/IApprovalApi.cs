using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api
{
    public interface IApprovalApi
    {
        [Get("/ManageWorkflow/GetAllAssignedRoles/{roleId}")]
        Task<IEnumerable<ManageWorkflow>> GetAllAssignedRolesAsync(int roleId);

        [Get("/ManageWorkflow/GetAllAssignedUserRoles/{roleId}")]
        Task<IEnumerable<ManageWorkflow>> GetAllAssignedUserRolesAsync(int roleId);
    }
}