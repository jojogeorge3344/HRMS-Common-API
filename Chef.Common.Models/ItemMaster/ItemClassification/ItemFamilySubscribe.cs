using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemFamilySubscribe : SubscribeModel
    {
        [ForeignKey("ItemFamily")]
        [Required]
        public int ItemFamilyId { get; set; }
    }
}
