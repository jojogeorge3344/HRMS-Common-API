using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class ItemClass : Model
    {
        [ForeignKeyId(typeof(ItemFamily))]
        [Required]
        public int ItemFamilyId { get; set; }
        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        public string ItemClassCode { get; set; } = string.Empty;
        [Required]
        [Unique(true)]
        public string ItemClassName { get; set; }
    }
}
