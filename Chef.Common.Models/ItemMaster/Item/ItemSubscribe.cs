using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemSubscribe : SubscribeModel
    {

        [ForeignKey("Item")]
        [Required]
        public int ItemId { get; set; }
    }
}
