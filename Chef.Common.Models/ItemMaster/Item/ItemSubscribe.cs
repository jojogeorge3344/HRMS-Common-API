using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class ItemSubscribe : SubscribeModel
    {

        [ForeignKeyId(typeof(Item))]
        [Required]
        public int ItemId { get; set; }
    }
}
