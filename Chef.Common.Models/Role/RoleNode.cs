using Chef.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Chef.Common.Models
{
   public class RoleNode:Model
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Node")]
        public int NodeId { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        [Write(false)]
        [Skip(true)]
        public List<RoleNodePermission> RoleNodePermissionList { get; set; }
    }
}
