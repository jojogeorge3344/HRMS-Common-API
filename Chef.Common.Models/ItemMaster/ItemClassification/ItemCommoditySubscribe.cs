using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemCommoditySubscribe : SubscribeModel
    {

        [ForeignKey("ItemCommodity")]
        [Required]
        public int ItemCommodityId { get; set; }
    }
}
