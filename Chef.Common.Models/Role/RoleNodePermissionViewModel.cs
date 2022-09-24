using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class RoleNodePermissionViewModel
    {
        public int SubModuleID { get; set; }
        public string SubModuleName { get; set; }
        public IList<RoleNodePermission> SubModulePermissions { get; set; }
    }
}