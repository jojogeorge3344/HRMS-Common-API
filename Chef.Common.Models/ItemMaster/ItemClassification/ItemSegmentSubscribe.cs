using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemSegmentSubscribe : SubscribeModel
    {

        [ForeignKey("ItemSegment")]
        [Required]
        public int ItemSegmentId { get; set; }
    }
}
