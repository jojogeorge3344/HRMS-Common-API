using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemClassSubscribe : SubscribeModel
    {

        [ForeignKey("ItemClass")]
        [Required]
        public int ItemClassId { get; set; }
    }
}
