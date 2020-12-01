using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class ItemSegmentSubscribe : SubscribeModel
    {

        [ForeignKeyId(typeof(ItemSegment))]
        [Required]
        public int ItemSegmentId { get; set; }
    }
}
