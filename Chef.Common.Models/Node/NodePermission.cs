using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class NodePermission : Model
    {       
        [ForeignKey("Node")]
        public int NodeId { get; set; }
       
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
    }
}
