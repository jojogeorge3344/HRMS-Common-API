using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class RoleNodePermission : Model
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("Node")]
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int ModuleId { get; set; }
        [Write(false)]
        [Skip(true)]
        public string ModuleName { get; set; }

        [ForeignKey("NodePermission")]
        public int NodePermissionId { get; set; }
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        [Write(false)]
        [Skip(true)]
        public int isactive { get; set; }

    }
}
