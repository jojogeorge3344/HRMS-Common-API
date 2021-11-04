using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemCommodity : Model
    {
        [ForeignKeyId(typeof(ItemClass))]
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
