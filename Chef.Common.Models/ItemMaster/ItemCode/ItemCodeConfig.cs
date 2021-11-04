using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class ItemCodeConfig : Model
    {
        [Required]
        [Unique(true)]
        public ItemCodeType ItemCodeType { get; set; }

        [Required]
        public string CodeFormat { get; set; }

        public int? MaxCounter { get; set; }
    }
}
