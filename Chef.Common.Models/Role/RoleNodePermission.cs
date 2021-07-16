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


        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        [Write(false)]
        [Skip(true)]
        public string PermissionName { get; set; }




    }
}
