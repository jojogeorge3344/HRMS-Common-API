using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class ItemFamilySubscribe : SubscribeModel
    {
        [ForeignKeyId(typeof(ItemFamily))]
        [Required]
        public int ItemFamilyId { get; set; }
    }
}
