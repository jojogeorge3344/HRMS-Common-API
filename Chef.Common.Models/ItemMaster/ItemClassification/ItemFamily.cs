using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemFamily : Model
    {
        [ForeignKeyId(typeof(ItemSegment))]
        [Required]
        public int ItemSegmentId { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        public string ItemFamilyCode { get; set; } = string.Empty;

        [Required]
        [Unique(true)]
        public string ItemFamilyName { get; set; }
    }
}
