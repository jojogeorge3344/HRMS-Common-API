using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
   public class NodeControlField:Model
    {
        [ForeignKey("Node")]
        public int NodeId { get; set; }

        public string ColumnName { get; set; }
        public string ColumnLabel { get; set; }

        public string DataType { get; set; }
    }
}
