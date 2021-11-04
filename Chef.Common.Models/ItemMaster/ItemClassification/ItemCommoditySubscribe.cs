using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemCommoditySubscribe : SubscribeModel
    {
        [ForeignKeyId(typeof(ItemCommodity))]
        [Required]
        public int ItemCommodityId { get; set; }
    }
}
