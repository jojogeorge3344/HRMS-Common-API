using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

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
