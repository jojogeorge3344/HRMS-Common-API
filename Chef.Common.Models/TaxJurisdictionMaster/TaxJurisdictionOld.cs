using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class TaxJurisdictionOld : Model
    {
        [Required]
        [Unique(true)]
        [Code]
        public string Code { get; set; }

        [Required]
        [Unique(true)]
        public string Name { get; set; }
    }
}
