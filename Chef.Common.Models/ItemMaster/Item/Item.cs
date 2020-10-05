using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class Item : Common.Core.RevisionModel
    {
        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        public string ItemCode { get; set; } = string.Empty;
        [Required]
        [Unique(true), Composite(Index =2)]
        public string ItemName { get; set; }
        [Required]
        public string ItemDescription { get; set; }

        [ForeignKeyId(typeof(ItemCommodity))]
        [Required]
        public int ItemCommodityId { get; set; }
    }
}
