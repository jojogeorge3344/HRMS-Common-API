using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemClassSubscribe : SubscribeModel
    {

        [ForeignKeyId(typeof(ItemClass))]
        [Required]
        public int ItemClassId { get; set; }
    }
}
