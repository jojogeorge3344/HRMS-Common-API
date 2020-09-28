using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemCommodity : Model
    {
        [ForeignKey("ItemClass")]
        [Required]
        public int ItemClassId { get; set; }
        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        public string ItemCommodityCode { get; set; } = string.Empty;
        [Required]
        [Unique(true)]
        public string ItemCommodityName { get; set; }
    }
}
