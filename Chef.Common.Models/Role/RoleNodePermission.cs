using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class RoleNodePermission : Model
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("NodePermission")]
        public int NodePermissionId { get; set; }

    }
}
