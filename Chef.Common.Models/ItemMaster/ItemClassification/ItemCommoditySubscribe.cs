using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemCommoditySubscribe : SubscribeModel
    {

        [ForeignKeyId(typeof(ItemCommodity))]
        [Required]
        public int ItemCommodityId { get; set; }
    }
}
