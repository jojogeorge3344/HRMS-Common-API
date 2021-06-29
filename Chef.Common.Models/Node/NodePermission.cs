using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
