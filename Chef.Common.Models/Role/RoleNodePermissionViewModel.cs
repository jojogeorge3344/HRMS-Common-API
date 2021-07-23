using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class RoleNodePermissionViewModel
    {
        public int SubModuleID { get; set; }
        public string SubModuleName { get; set; }
        public IList<RoleNodePermission> SubModulePermissions { get; set; }
    }
}
