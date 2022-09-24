using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class RoleViewModel
    {
        public int RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public IList<Node> ModuleList { get; set; }
        public IList<RoleNodePermissionViewModel> Submodules { get; set; }
    }
}
