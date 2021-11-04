using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemClassSubscribe : SubscribeModel
    {
        [ForeignKeyId(typeof(ItemClass))]
        [Required]
        public int ItemClassId { get; set; }
    }
}
