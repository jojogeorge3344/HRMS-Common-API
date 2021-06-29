using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;


namespace Chef.Common.Models
{
   public class RoleNode:Model
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Node")]
        public int NodeId { get; set; }
    }
}
