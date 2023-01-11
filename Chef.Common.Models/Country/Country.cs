using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Country : Model
    {
        [Required]
        [StringLength(126)]
        public string Name { get; set; }

        [Required]
        [StringLength(4)]
        public string Code { get; set; }

        public string DialCode { get; set; }
        public int CountryMasterId { get; set; }
    }
}
