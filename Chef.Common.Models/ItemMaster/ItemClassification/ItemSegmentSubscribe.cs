using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemSegmentSubscribe : SubscribeModel
    {
        [ForeignKeyId(typeof(ItemSegment))]
        [Required]
        public int ItemSegmentId { get; set; }
    }
}